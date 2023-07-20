using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealthBarUIOnChange : MonoBehaviour
{


	public HealthManager healthManager;
	public RectTransform healthRect;

	float originalWidth = 1f;
	void Start()
	{
		originalWidth = healthRect.rect.width;
		if (healthManager != null) healthManager.onHealthManagerChange += OnHealthChange;
	}

	void OnDestroy()
	{
		if (healthManager != null) healthManager.onHealthManagerChange -= OnHealthChange;
	}
	
	public void OnHealthChange(float currHealth)
	{
		healthRect.sizeDelta = new Vector2 (originalWidth * currHealth / healthManager.maxHealth, healthRect.rect.height);
	}
}