using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionWithTagSetScore : MonoBehaviour
{
    public string Tag = "";
    public int NewScore = 0;

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
            scoreManager.SetScore(NewScore);
        }
    }

    void Update()
    {

    }
}
