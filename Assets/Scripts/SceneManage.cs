using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject Niveles;
    public int nivelActual=0;
    private GameObject nivel;
    private void Start() {
        nivel = Niveles.transform.GetChild(nivelActual).gameObject;
    }
    public void NextScene()
    {
        //if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        nivel.SetActive(false);
        nivelActual += 1;
        GetComponent<GameHandler>().Finish();
        if (nivelActual >= Niveles.transform.childCount)
        {
            Application.Quit();
            Debug.Log("QUIT");
        }
        else
        {
            nivel = Niveles.transform.GetChild(nivelActual).gameObject;
            nivel.SetActive(true);
        }
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
