using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelfIfLeaveCamera : MonoBehaviour {

	void KillSelf()
	{
		if (gameObject.GetComponent<HealthManager>()) {GetComponent<HealthManager>().Kill();}
		else 
		{
			Destroy(gameObject);
			gameObject.SetActive(false);
		}

	}

	void OnBecameInvisible()
	{
		KillSelf();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
