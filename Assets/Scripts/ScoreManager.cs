using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highScoreText;

    int score = 0, highScore = 0;

    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + " POINTS";
        highScoreText.text = "High Score: "+highScore.ToString();
        
    }

   public void AddPoint()
    {
        score = score + Random.Range(10, 20);
        scoreText.text = score.ToString() + " POINTS";
        
        if(highScore<score)
        PlayerPrefs.SetInt("highscore", score);
    }
    
}
