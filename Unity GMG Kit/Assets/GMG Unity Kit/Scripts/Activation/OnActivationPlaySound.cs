using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActivationPlaySound : ActivatableBehaviour {

	// Use this for initialization

	public AudioClip sound;
	AudioSource audioSrc;
	public bool Loop = false;
	void Start () {
		audioSrc = gameObject.AddComponent<AudioSource>();
	}
	
	public override void onActivate(bool activated)
	{
		if (activated)
		{
			audioSrc.clip = sound;
			audioSrc.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
