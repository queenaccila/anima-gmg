using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerCrouch : MonoBehaviour {

	// Use this for initialization
	Animator animator;
	InputManager inputMgr;
	GameManager gameMgr;
	
	public AudioClip soundFile;
	public bool LoopSound = false;
	AudioSource audioSrc;
	void Start () 
	{
		audioSrc = gameObject.AddComponent<AudioSource>();
		audioSrc.clip = soundFile;
		audioSrc.loop = LoopSound;

		animator = GetComponent<Animator>();
		gameMgr = GameManager.Inst();
		gameMgr.onPause += OnPause;
		inputMgr = gameMgr.InputManager();
	}

	void OnDestroy()
	{
		gameMgr.onPause -= OnPause;
	}
	
	public void OnPause(bool pause)
	{
		if (pause) audioSrc.Stop();
	}	
	// Update is called once per frame
	Vector3 bufferScale;
	bool crouched = false;
	void Update () 
	{
		if (gameMgr.isPaused) return;
		
		if (!crouched) bufferScale = transform.localScale;
		if (inputMgr.GetDown())
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			crouched = true;
			transform.localScale= new Vector3(bufferScale.x, 0.5f*bufferScale.y, bufferScale.z);
			if (animator != null) animator.SetBool("crouching", true);
		}
		else
		{
			audioSrc.Stop();
			crouched = false;
			transform.localScale= bufferScale;
			if (animator != null) animator.SetBool("crouching", false);
		}
	}
}
