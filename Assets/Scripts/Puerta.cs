using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public bool open = false;
    public GameObject sceneManager;

    private void Start() {
        open = false;
        GetComponent<Animator>().SetBool("Abrir", open);
        GetComponent<BoxCollider2D>().isTrigger = true;
        sceneManager = GameObject.Find("GameHandler");
    }

    public void Abrir()
    {
        open = true;
        GetComponent<Animator>().SetBool("Abrir", open);
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Player"))
            sceneManager.GetComponent<SceneManage>().NextScene();
    }
}
