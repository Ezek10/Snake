using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;
    private int maxScore;
    public Text texto;
    private int record;
    public int goal = 3;
    void Start()
    {
        texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
        maxScore = PlayerPrefs.GetInt("record",-3);
    }

    // Update is called once per frame
    public void RaiseScore(int s)
    {
        score += s;
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("record",maxScore);
        }
        texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
        if (score == goal)
            GetComponent<GameHandler>().GoalReached();
            
    }
    public void DeleteScore()
    {
        score = 0;
        texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
    }

}
