using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActivationKillSelf : ActivatableBehaviour {

	// Use this for initialization
	
	public override void onActivate(bool activated)
	{
		if (activated)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
