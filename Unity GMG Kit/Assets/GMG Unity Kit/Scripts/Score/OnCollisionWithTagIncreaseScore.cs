using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionWithTagIncreaseScore : MonoBehaviour
{
    public string Tag = "";
    public int AmountToAdd = 0;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag != Tag) return;

        ScoreManager scoreManager = GetComponent<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("No Score Manager provided");
        }
        else
        {
            scoreManager.IncreaseScore(AmountToAdd);
        }
    }

    void Update()
    {

    }
}
