using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellularBoolGrid : MonoBehaviour
{
    public bool[,,] cellMap;
    public int width = 10;
    public int height = 10;
    public int depth = 10;
    public GameObject caveMesh;

    float chanceToStartAlive = 0.45f;

    void Start()
    {
        cellMap = new bool[width, height, depth];
        cellMap = InitialiseMap(cellMap);
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

                        if (map[x, y, z] == true)
                        {
                            GameObject.Instantiate(caveMesh, new Vector3(x, y, z), Quaternion.identity, transform);
                        }
                    }
                }
            }
        }
        return map;
    }

    public calculateNewCells(bool[,,] oldMap)
    {
        bool[,,] newMap = new bool[width, height, depth];
    }

    public countAliveNeighbors(bool[,,] map, int x, int y, int z)
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
                    
                    if(i == 0 && j == 0 && k == 0)
                    {
                        return;
                    }

                    else if(neighbor_x < 0 || neighbor_y < 0 || neighbor_z < 0)
                }
            }
        }
    }
}

