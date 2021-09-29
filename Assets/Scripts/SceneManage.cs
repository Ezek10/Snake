using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount)
            {
                Application.Quit();
                Debug.Log("QUIT");
            }
    }
}
