using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeathLoadScene : MonoBehaviour
{

	public float waitTimeBeforeReload = 1f;
	public string sceneName = "";
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
				UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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