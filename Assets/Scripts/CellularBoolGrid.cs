using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellularBoolGrid : MonoBehaviour
{
    [Header("Map Values")]
    public bool[,,] cellMap;
    public int width = 10;
    public int height = 10;
    public int depth = 10;

    public GameObject caveMesh;

    [Header("Simulation Values")]
    public int numberOfSteps = 0;
    public int deathLimit = 0;
    public int birthLimit = 0;

    public float chanceToStartAlive = 0.45f;

    void Start()
    {
        cellMap = new bool[width, height, depth];
        cellMap = InitialiseMap(cellMap);
        for(int i=0; i<numberOfSteps; i++)
        {
            cellMap = DoSimulationStep(cellMap);
        }
        StartCoroutine(GenerateMapMesh(cellMap));
    }

    IEnumerator GenerateMapMesh(bool[,,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    if (map[x, y, z] == true)
                    {
                        GameObject.Instantiate(caveMesh, new Vector3(x, y, z), Quaternion.identity, transform);
                        yield return null; //new WaitForSeconds(.001f);
                    }
                }
            }
        }
    }

    public bool[,,] InitialiseMap(bool[,,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    if (Random.value < chanceToStartAlive)
                    {
                        map[x, y, z] = true;
                    }
                }
            }
        }
        return map;
    }

    public bool[,,] DoSimulationStep(bool[,,] oldMap)
    {
        bool[,,] newMap = new bool[width, height, depth];

        //loop over each axis of map
        for(int x=0; x < oldMap.GetLength(0); x++)
        {
            for (int y = 0; y < oldMap.GetLength(1); y++)
            {
                for (int z = 0; z < oldMap.GetLength(2); z++)
                {
                    int numAiveNeighbors = CountAliveNeighbors(oldMap, x, y, z);
                    Debug.Log(numAiveNeighbors);
                    //New value based on sim rules
                    //if cell is alive but has too few neibhors kill it
                    if (oldMap[x, y, z])
                    {
                        if(numAiveNeighbors < deathLimit)
                        {
                            newMap[x, y, z] = false;
                        }
                        else
                        {
                            newMap[x, y, z] = true;
                        }
                    }
                    //if cell is dead, check if it has the right num of neighbors to be born
                    else
                    {
                        if(numAiveNeighbors > birthLimit)
                        {
                            newMap[x, y, z] = true;
                        }
                        else
                        {
                            newMap[x, y, z] = false;
                        }
                    }
                }
            }
        }
        return newMap;
    }

    public int CountAliveNeighbors(bool[,,] map, int x, int y, int z)
    {
        int count = 0;
        for(int i = -1; i<2; i++)
        {
            for(int j=-1; j<2; j++)
            {
                for(int k=-1; k<2; k++)
                {
                    int neighbor_x = x + i;
                    int neighbor_y = y + j;
                    int neighbor_z = z + k;
                    
                    //do nothing, do not add to it self
                    if(i == 0 && j == 0 && k == 0)
                    {
                        
                    }

                    //check if index is off edge
                    else if(neighbor_x < 0 || neighbor_y < 0 || neighbor_z < 0 || neighbor_x >= map.GetLength(0) || neighbor_y >= map.GetLength(1) || neighbor_z >= map.GetLength(2))
                    {
                        count += 1;
                    }

                    //normal check of nehiborhood
                    else if (map[neighbor_x, neighbor_y, neighbor_z])
                    {
                        count += 1;
                    }
                    
                }
            }
        }

        return (count);
    }
}

