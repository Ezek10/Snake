using System;
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
    public GameObject puerta;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    public GameObject pantallaDePausa;
    private List<IOnTickListener> listeners = new List<IOnTickListener>();
    private Queue<Direction> keyQueue = new Queue<Direction>();
    private bool gamePause = false;
    public int cantidadDeComida = 1;
    private Collider2D debug;
    private bool finish = false;
    void Start()
    {
        _instance = this;
        Initiate();
        keyQueue.Enqueue(Direction.Right);
        gridMoveTimerMax = .10f;
        gridMoveTimer = gridMoveTimerMax;
        puerta = GameObject.Find("Puerta");
        comidaConteiner = GameObject.Find("Comida");
        pantallaDePausa = GameObject.Find("Pantalla de Pausa");
        pantallaDePausa.transform.GetChild(0).gameObject.SetActive(true);
        pantallaDePausa.SetActive(false);
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
        
        if (comidaConteiner.transform.childCount < cantidadDeComida && finish == false)
        {
            float x = UnityEngine.Random.Range(1, 25);
            float y = UnityEngine.Random.Range(1, 20);
            Vector3 position = new Vector3(x, y, 0);
            Quaternion rotacion = new Quaternion(0, 0, 0, 0);
            //for(int i = 0; i < serpiente.GetComponent<Snake>().lista.Count ; i++)
                //serpiente.GetComponent<Snake>().lista[i].transform.position.x;
            conejoVivo = Instantiate(conejo, position, rotacion);
            conejoVivo.transform.SetParent(comidaConteiner.transform);
            //gameObject.GetComponent<Score>().RaiseScore(1);
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
        if (PauseHandle())
            {
                HandleKey();
                TimeHandle();
            }
    }
    public void GoalReached()
    {
        finish = true;
        puerta.GetComponent<Puerta>().Abrir();
    }
    public void Finish(bool dead=false){
        finish = !finish;
        if (dead)
        {
            Debug.Log("MUERTE");
            Destroy(gameObject);
        }
    }
    private bool PauseHandle()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            {
                pantallaDePausa.SetActive(!pantallaDePausa.activeSelf);
                gamePause = !gamePause;
                gameObject.GetComponent<AudioSource>().mute = !gameObject.GetComponent<AudioSource>().mute;
            }
        return !gamePause;
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
