using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollisionTag : MonoBehaviour
{
	public string Tag = "";
	public float Damage = 10f;
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.collider.tag != Tag) return;
		HealthManager healthManager = coll.collider.gameObject.GetComponent<HealthManager>();
		if (healthManager != null) healthManager.TakeDamage(Damage);
	}

	void Start()
	{
	}


}