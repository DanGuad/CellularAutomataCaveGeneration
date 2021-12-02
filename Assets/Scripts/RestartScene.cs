using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            OnRestart();
        }
    }

    void OnRestart()
    {
        Debug.Log("Kill me this is nuts");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
