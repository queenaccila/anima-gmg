using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int InitialScore = 0;
    public int currScore;

    void Awake()
    {
        ChangeScore(InitialScore);
    }

    void ChangeScore(int amount)
    {
        currScore = amount;
        if (onScoreManagerChange != null) onScoreManagerChange(currScore);
    }

    public void SetScore(int amount)
    {
        ChangeScore(amount);
    }

    public void IncreaseScore(int amount)
    {
        ChangeScore(currScore + amount);
    }

    public void DecreaseScore(int amount)
    {
        ChangeScore(currScore - amount);
    }

    public int GetCurrScore()
    {
        return currScore;
    }

    public delegate void ScoreManagerChangeHandler(int amount);
    public event ScoreManagerChangeHandler onScoreManagerChange;
}
