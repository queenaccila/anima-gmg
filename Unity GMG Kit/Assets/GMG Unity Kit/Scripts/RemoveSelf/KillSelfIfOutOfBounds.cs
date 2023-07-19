using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelfIfOutOfBounds : MonoBehaviour {

	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	void KillSelf()
	{
		if (gameObject.GetComponent<HealthManager>()) {GetComponent<HealthManager>().Kill();}
		else 
		{
			Destroy(gameObject);
			gameObject.SetActive(false);
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = transform.position.x;
		float y = transform.position.y;

		if (x > maxX || x < minX || y > maxY || y < minY) KillSelf();
	}
}
