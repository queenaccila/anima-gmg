using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSelfOnScoreChange : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int ActivationAmount = 0;

    Activatable activatable;
	void Start()
	{
		activatable = gameObject.GetComponent<Activatable>();
		if (activatable == null) Debug.LogError("No OnActivation/OnDeactivation scripts added");
		
		if (scoreManager == null)
        {
            Debug.LogError("No Score Manager provided");
        }
        else
        {
            if (scoreManager != null) scoreManager.onScoreManagerChange += onScoreChange;
        }
	}

	void OnDestroy()
	{
		if (scoreManager != null) scoreManager.onScoreManagerChange -= onScoreChange;
	}
    
    void onScoreChange(int amount)
    {
    	if (amount == ActivationAmount)  activatable.Activate();
    }
}
