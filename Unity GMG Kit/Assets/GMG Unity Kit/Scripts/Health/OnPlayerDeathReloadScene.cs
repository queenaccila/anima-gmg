using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeathReloadScene : MonoBehaviour
{

	public float waitTimeBeforeReload = 0f;

	HealthManager healthManager;
	void Start()
	{
		healthManager = GetComponent<HealthManager>();
		if (healthManager != null) healthManager.onHealthManagerDeath += OnPlayerDeath;
	}

	void OnDestroy()
	{
		if (healthManager != null) healthManager.onHealthManagerDeath -= OnPlayerDeath;
	}

	float waitTimeCounter = 0f;
	void Update()
	{
		if (startReload)
		{
			if(waitTimeBeforeReload <= waitTimeCounter)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
			}

			waitTimeCounter += Time.deltaTime;
		}
	}	

	bool startReload = false;
	public void OnPlayerDeath()
	{
		startReload = true;
	}
}