using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeactivationDecreaseScore : ActivatableBehaviour
{
    public ScoreManager scoreManager;
    public int AmountToRemove = 0;

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
                scoreManager.DecreaseScore(AmountToRemove);
            }
        }
    }
}
