using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSelfOnCollisionTag : MonoBehaviour
{
	public string Tag = "";
	public float HealAmount = 10f;
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.collider.tag != Tag) return;
		HealthManager healthManager = GetComponent<HealthManager>();
		if (healthManager != null) healthManager.Heal(HealAmount);
	}

	void Start()
	{
	}


}