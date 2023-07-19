using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeactivationIncreaseScore : ActivatableBehaviour
{
    public ScoreManager scoreManager;
    public int AmountToAdd = 0;

    public override void onActivate(bool activated)
    {
        if (!activated)
        {
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
}
