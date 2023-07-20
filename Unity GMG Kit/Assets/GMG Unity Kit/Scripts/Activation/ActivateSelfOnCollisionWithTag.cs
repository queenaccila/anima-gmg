using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSelfOnCollisionWithTag : MonoBehaviour {

	public string TagName = "";
	bool activate = false;

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.tag == TagName)
		{
			activatable.Activate();
		}
	}

	Activatable activatable;
	void Start()
	{
		activatable = gameObject.GetComponent<Activatable>();
		if (activatable == null) Debug.LogError("No OnActivation/OnDeactivation scripts added");
	}

	// Update is called once per frame
	void Update () 
	{
	}
}
