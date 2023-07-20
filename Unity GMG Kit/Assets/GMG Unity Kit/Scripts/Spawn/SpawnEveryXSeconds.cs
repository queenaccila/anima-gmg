using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEveryXSeconds : MonoBehaviour {

	public Object prefabToSpawn;
	public float timeToSpawn = 5f;
	// Use this for initialization
	public AudioClip soundFile;
	public bool LoopSound = false;
	AudioSource audioSrc;
	void Start () 
	{
		audioSrc = gameObject.AddComponent<AudioSource>();
		audioSrc.clip = soundFile;
		audioSrc.loop = LoopSound;
	}
	
	// Update is called once per frame
	float counter = 0f;
	void Update () {
		if (counter >= timeToSpawn)
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			Instantiate(prefabToSpawn, transform);
			counter = 0f;
		}

		counter += Time.deltaTime;
	}
}
