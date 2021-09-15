using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;
    private int maxScore = 0;
    public Text texto;
    void Start()
    {
        texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
    }

    // Update is called once per frame
    public void RaiseScore(int s)
    {
        score += s;
        if (score > maxScore)
            maxScore = score;
        texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
    }
    public void DeleteScore()
    {
        score = 0;
        texto.text = "Score: " + score.ToString() + "\nMaxScore: " + maxScore.ToString();
    }
}
