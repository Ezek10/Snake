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
    public int goal = 15;
    public bool maxEnable = false;
    void Start()
    {
        texto = GameObject.Find("Score").GetComponent<Text>();
        if (maxEnable)
        {
            texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
            maxScore = PlayerPrefs.GetInt("record",-3);
        }
        else
        {
            texto.text = "Score: " + score.ToString() + "\nGoal: " + goal.ToString();
        }
    }

    // Update is called once per frame
    public void RaiseScore(int s)
    {
        score += s;
        if(maxEnable)
        {
            if (score > maxScore)
            {
                maxScore = score;
                PlayerPrefs.SetInt("record",maxScore);
            }
            texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
        }
        else
        {
            texto.text = "Score: " + score.ToString() + "\nGoal: " + goal.ToString();
            if (score == goal)
                GetComponent<GameHandler>().GoalReached();
        }
    }
    public void DeleteScore()
    {
        score = 0;
        if (maxEnable)
            texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
        else
            texto.text = "Score: " + score.ToString() + "\nGoal: " + goal.ToString();
    }

}
