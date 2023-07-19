using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledTimeParticle : MonoBehaviour {

	ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
		particleSystem = this.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		particleSystem.Simulate(Time.unscaledDeltaTime, true, false);
	}
}
