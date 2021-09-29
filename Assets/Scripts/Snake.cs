using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour, IOnTickListener
{
    private Vector2Int gridPosition, gridMoveDirection;
    private new AudioSource audio;
    private bool cuerpoNuevo = false;
    public AudioClip eat, die;
    public GameObject cuerpo;
    public Sprite spriteCola, spriteCuerpo, spriteCodo;
    private GameObject gameHandler;
    public List<GameObject> lista = new List<GameObject>();
    void Start()
    {
        gameHandler = GameObject.Find(nameof(GameHandler));
        GameHandler.Instance.registerOnTickListener(this);
        audio = gameObject.GetComponent<AudioSource>();
        gridPosition = new Vector2Int(10, 10);
    }
    public void onTick(Direction move)
    {
        Direccion(move);
        Movimiento();
        Sprites();
        HeadRotation();
    }
    private void Direccion(Direction nextMove)
    {
        switch (nextMove)
        {
            case Direction.Up:
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
                break;
            case Direction.Down:
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
                break;
            case Direction.Left:
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
                break;
            case Direction.Right:
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
                break;
            case Direction.Undefined:
                break;
        }

    }
    private void Movimiento()
    {
        for (int i = lista.Count - 1; i > 0; i--)
        {
            if (cuerpoNuevo == true)
            {
                lista.Add(Instantiate(cuerpo, lista[lista.Count - 1].transform.position, Quaternion.identity));
                lista[lista.Count - 1].GetComponent<SpriteRenderer>().sprite = spriteCola;
                lista[lista.Count - 1].transform.parent = gameObject.transform;
                lista[lista.Count - 2].GetComponent<SpriteRenderer>().sprite = spriteCuerpo;
                cuerpoNuevo = false;
            }
            lista[i].transform.position = lista[i - 1].transform.position;
        }
        gridPosition += gridMoveDirection;
        lista[0].transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }
    private void Sprites()
    {
        int i = lista.Count - 1;
        Transform temp;
        Transform temp2 = lista[lista.Count - 1].transform;
        
        while (i > 0)
        {
            if (i != lista.Count - 1)
                temp2 = lista[i + 1].transform;
            temp = lista[i - 1].transform;

            if (temp.position.x > lista[i].transform.position.x)
            {
                if (temp2.transform.position.y > lista[i].transform.position.y & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(0f, 0, 0);
                }
                else if (temp2.transform.position.y < lista[i].transform.position.y & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(180f, 0, 0);
                }
                else if (temp2.transform.position.y == lista[i].transform.position.y)
                {
                    lista[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCuerpo;
                }
            }
            else if (temp.position.x < lista[i].transform.position.x)
            {
                if (temp2.transform.position.y > lista[i].transform.position.y & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(0f, 180f, 0);
                }
                else if (temp2.transform.position.y < lista[i].transform.position.y & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(180f, 180f, 0);
                }
                else if (temp2.transform.position.y == lista[i].transform.position.y)
                {   
                    lista[i].transform.rotation = Quaternion.Euler(0, 0, 180f);
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCuerpo;
                }
            }
            else if (temp.position.y > lista[i].transform.position.y)
            {
                if (temp2.transform.position.x > lista[i].transform.position.x & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(0f, 0, 0);
                }
                else if (temp2.transform.position.x < lista[i].transform.position.x & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(0f, 0, 90f);
                }
                else if (temp2.transform.position.x == lista[i].transform.position.x)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCuerpo;
                    lista[i].transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
            }
            else if (temp.position.y < lista[i].transform.position.y)
            {
                if (temp2.transform.position.x > lista[i].transform.position.x & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(180f, 0, 0);
                }
                else if (temp2.transform.position.x < lista[i].transform.position.x & i != lista.Count - 1)
                {
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCodo;
                    lista[i].transform.rotation = Quaternion.Euler(180f, 180f, 0);
                }
                else if (temp2.transform.position.x == lista[i].transform.position.x)
                {
                    lista[i].transform.rotation = Quaternion.Euler(0, 0, 270f);
                    lista[i].GetComponent<SpriteRenderer>().sprite = spriteCuerpo;
                }
            }
            lista[lista.Count - 1].GetComponent<SpriteRenderer>().sprite = spriteCola;
            i--;
        }
    }
    private void HeadRotation()
    {
        if (gridMoveDirection.x == 1)
            lista[0].transform.rotation = Quaternion.Euler(0, 0, 270f);
        else if (gridMoveDirection.x == -1)
            lista[0].transform.rotation = Quaternion.Euler(0, 0, 90f);
        else if (gridMoveDirection.y == 1)
            lista[0].transform.rotation = Quaternion.Euler(0, 0, 0f);
        else if (gridMoveDirection.y == -1)
            lista[0].transform.rotation = Quaternion.Euler(0, 0, 180f);
    }
    public Direction GetCurrentDirection()
    {
        if (gridMoveDirection.y == 1) return Direction.Up;
        else if (gridMoveDirection.y == -1) return Direction.Down;
        else if (gridMoveDirection.x == -1) return Direction.Left;
        else if (gridMoveDirection.x == 1) return Direction.Right;
        else return Direction.Undefined;
    }
    public void Eat(GameObject Comida)
    {
        GameObject.Find("GameHandler").GetComponent<Score>().RaiseScore(1);
        Destroy(Comida);
        cuerpoNuevo = true;
        audio.clip = eat;
        audio.Play();
    }
    public void Die()
    {
        audio.clip = die;
        audio.Play();
        GameHandler.Instance.exitOnTickListener(this);
        Destroy(gameObject);
    }
}
