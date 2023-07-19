using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathRemoveSelf : MonoBehaviour
{
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

	public void OnPlayerDeath()
	{
		Destroy(gameObject);
		gameObject.SetActive(false);
	}
}