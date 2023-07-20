using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelfAfterCollision : MonoBehaviour {

	public float timeTillDeath = 0f;
	float deathTimer = 0f;
	bool dying = false;
	void OnCollisionEnter2D(Collision2D coll)
	{
		dying = true;
		GetComponent<Collider2D>().enabled = false;
    }
	void OnTriggerEnter2D(Collider2D coll)
	{
		dying = true;
		GetComponent<Collider2D>().enabled = false;
    }
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
		if (dying)
		{
			if (deathTimer >= timeTillDeath)
			{
				KillSelf();
			}
			deathTimer += Time.deltaTime;
		}
	}
}
