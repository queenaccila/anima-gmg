using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerIncreaseScoreWithTag : MonoBehaviour
{
    public string Tag = "";
    public int AmountToAdd = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != Tag) return;

        ScoreManager scoreManager = collision.gameObject.GetComponent<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("No Score Manager provided");
        }
        else
        {
            scoreManager.IncreaseScore(AmountToAdd);
        }
    }

}
