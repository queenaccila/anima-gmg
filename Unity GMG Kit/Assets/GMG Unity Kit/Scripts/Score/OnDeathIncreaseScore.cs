using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathIncreaseScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int AmountToAdd = 0;
	HealthManager healthManager;

    void Start()
	{
		healthManager = GetComponent<HealthManager>();
		if (healthManager != null) healthManager.onHealthManagerDeath += OnPlayerDeath;
	}

	void OnDestroy()
	{
		if (healthManager != null) healthManager.onHealthManagerDeath -= OnPlayerDeath;
		ChangeScore();
	}

    public void OnPlayerDeath()
	{
		ChangeScore();
	}

	bool scoreChanged = false;
	void ChangeScore()
	{
		if (scoreChanged) return;

		if (scoreManager == null)
        {
            Debug.LogError("No Score Manager provided");
        }
        else
        {
            scoreManager.IncreaseScore(AmountToAdd);
        }
		scoreChanged = true;
	}
}
