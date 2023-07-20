using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
                 
public class UpdateScoreUIOnChange : MonoBehaviour
{
    public ScoreManager scoreManager;
    Text displayScore;

    void Start()
    {
        displayScore = GetComponent<Text>();
        displayScore.text = "Score: " + scoreManager.GetCurrScore().ToString();
        if (scoreManager != null) scoreManager.onScoreManagerChange += OnScoreChange;
    }

    void OnDestroy()
    {
    	if (scoreManager != null) scoreManager.onScoreManagerChange -= OnScoreChange;
    }

    public void OnScoreChange(int currScore)
    {
        displayScore.text = "Score: " + currScore.ToString();
    }
}