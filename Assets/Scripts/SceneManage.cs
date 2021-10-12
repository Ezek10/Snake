using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject niveles;
    private int nivelIndex;
    private GameObject nivel;
    public GameObject lugares;
    private int lugarIndex;
    private GameObject lugar;
    private void Start() {

        //LUGARES

        lugares.transform.GetChild(0).gameObject.SetActive(false);
        lugarIndex = PlayerPrefs.GetInt("Lugar",0);
        if (lugarIndex >= lugares.transform.childCount)
        {
            PlayerPrefs.SetInt("Lugar",0);
            lugarIndex = 0;
        }
        lugar = lugares.transform.GetChild(lugarIndex).gameObject;
        lugar.SetActive(true);

        //FORMAS

        niveles.transform.GetChild(0).gameObject.SetActive(false);
        nivelIndex = PlayerPrefs.GetInt("Nivel",0);
        if (nivelIndex >= niveles.transform.childCount)
        {
            PlayerPrefs.SetInt("Nivel",0);
            nivelIndex = 0;
        }
        nivel = niveles.transform.GetChild(nivelIndex).gameObject;
        nivel.SetActive(true);


        GetComponent<NivelActual>().NuevoNivel(nivel,lugar);
    }
    public void NextScene()
    {
        nivelIndex = PlayerPrefs.GetInt("Nivel") + 1;
        if (nivelIndex >= niveles.transform.childCount)
        {
            lugarIndex = PlayerPrefs.GetInt("Lugar") + 1;
            nivelIndex = 0;
        }
        GetComponent<GameHandler>().Finish();        
        if (lugarIndex >= lugares.transform.childCount)
        {
            GetComponent<GameHandler>().Finish(true);        
            lugarIndex=0;
            Application.Quit();
            Debug.Log("QUIT");
        }
        else
        {
            lugar.SetActive(false);
            nivel.SetActive(false);
            lugar = lugares.transform.GetChild(lugarIndex).gameObject;
            lugar.SetActive(true);
            
            nivel = niveles.transform.GetChild(nivelIndex).gameObject;
            nivel.SetActive(true);

            GetComponent<NivelActual>().NuevoNivel(nivel,lugar);
        }
        PlayerPrefs.SetInt("Nivel",nivelIndex);
        PlayerPrefs.SetInt("Lugar",lugarIndex);
    }
}
