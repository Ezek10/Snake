using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private GameObject snake;
    // Start is called before the first frame update
    private bool die = false;
    private void Start()
    {
        snake = gameObject.transform.parent.gameObject;
        
    }
    private void OnTriggerExit2D(Collider2D collision) {
        
        if (collision.transform.CompareTag("MainCamera") || collision.transform.CompareTag("Cuerpo"))
            snake.GetComponent<Snake>().Die(); 

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "comida")
        {
            snake.GetComponent<Snake>().Eat(collision.transform.gameObject);
        }
    }
}
