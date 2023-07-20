using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnSceneLoad : MonoBehaviour {

	public AudioClip soundFile;
	public bool Loop = true;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource>();
		if(soundFile != null) 
		{
			audioSource.clip = soundFile;
			audioSource.loop = Loop;
			audioSource.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
