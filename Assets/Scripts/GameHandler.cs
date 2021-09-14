﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;
    public static GameHandler Instance { get { return _instance; } }
    public GameObject conejo;
    public GameObject snake;
    private GameObject conejoVivo;
    public GameObject comidaConteiner;
    private GameObject serpiente;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private List<IOnTickListener> listeners = new List<IOnTickListener>();
    private Queue<Direction> keyQueue = new Queue<Direction>();
    void Start()
    {
        _instance = this;
        Initiate();
        keyQueue.Enqueue(Direction.Right);
        gridMoveTimerMax = .10f;
        gridMoveTimer = gridMoveTimerMax;
    }
    private void Initiate()
    {
        Vector3 position = new Vector3(0, 0, 0);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        serpiente = Instantiate(snake, position, rotation);
    }
    public void registerOnTickListener(IOnTickListener listener)
    {
        if(listener != null)
            listeners.Add(listener);
    }
    void cleanListeners()
    {
        listeners.Clear();
    }
    void Update()
    {
        //gridMoveTimer += Time.deltaTime;
        //if (gridMoveTimer > gridMoveTimerMax)
        //conejoVivo = GameObject.FindGameObjectWithTag("comida");
        
        if (comidaConteiner.transform.childCount == 0)
        {
            float x = UnityEngine.Random.Range(0, 25);
            float y = UnityEngine.Random.Range(0, 20);
            Vector3 position = new Vector3(x, y, 0);
            Quaternion rotacion = new Quaternion(0, 0, 0, 0);
            //for(int i = 0; i < serpiente.GetComponent<Snake>().lista.Count ; i++)
                //serpiente.GetComponent<Snake>().lista[i].transform.position.x;
            conejoVivo = Instantiate(conejo, position, rotacion);
            conejoVivo.transform.SetParent(comidaConteiner.transform);

            gameObject.GetComponent<Score>().RaiseScore(1);
        }

        if (serpiente == null)
        {
            gameObject.GetComponent<Score>().DeleteScore();
            Vector3 position = new Vector3(0, 0, 0);
            Quaternion rotation = new Quaternion(0, 0, 0, 0);
            keyQueue.Clear();
            serpiente = Instantiate(snake, position, rotation);
            keyQueue.Enqueue(Direction.Right);
        }

        HandleKey();
        TimeHandle();
    }
    private void TimeHandle()
    {
        gridMoveTimer += Time.deltaTime;
        if(gridMoveTimer >= gridMoveTimerMax)
        {
            foreach (var listener in listeners)
            {
                if (keyQueue.Count != 0)
                    listener.onTick(keyQueue.Dequeue());
                else
                    listener.onTick(Direction.Undefined);
            }
            gridMoveTimer -= gridMoveTimerMax;
        }
    }
    private void HandleKey()
    {
        
        Direction? selected = null;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            selected = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selected = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow))
        {
            selected = Direction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow))
        {
            selected = Direction.Up;
        }
        if(selected.HasValue & selected != serpiente.GetComponent<Snake>().GetCurrentDirection().Oposite())
        {
            keyQueue.Enqueue(selected.Value);
        }
    }

    public void exitOnTickListener(IOnTickListener listener)
    {
        if (listener != null)
            listeners.Remove(listener);
    }
}
