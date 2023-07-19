using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelfAfterTime : MonoBehaviour {

	public float timeTillDeath = 1f;
	float deathTimer = 0f;

	// Use this for initialization
	void Start () {
		
	}

	void KillSelf()
	{
		if (gameObject.GetComponent<HealthManager>()) {GetComponent<HealthManager>().Kill();}
		else 
		{
			Destroy(gameObject);
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (deathTimer >= timeTillDeath)
		{
			KillSelf();
		}
		deathTimer += Time.deltaTime;
	}
}
