using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerRun : MonoBehaviour {

	InputManager inputMgr;
	GameManager gameMgr;
	Animator animator;
	SpriteRenderer sprite;
	public float runSpeed = 1f;
	public bool cannotRunLeft = false;
	public bool cannotRunRight = false;

	public AudioClip soundFile;
	public bool LoopSound = true;
	AudioSource audioSrc;

	public void OnPause(bool pause)
	{
		if (pause) audioSrc.Stop();
	}

	void Start ()
	{
		gameMgr = GameManager.Inst();
		gameMgr.onPause += OnPause;
		inputMgr = gameMgr.InputManager();

		audioSrc = gameObject.AddComponent<AudioSource>();
		audioSrc.clip = soundFile;
		audioSrc.loop = LoopSound;
		
		animator = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
	}
	
	void OnDestroy()
	{
		gameMgr.onPause -= OnPause;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameMgr.isPaused) return;

		if (inputMgr.GetRight() && !cannotRunRight)
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			if (animator != null) animator.SetFloat("runSpeed", runSpeed);
			if (animator != null) animator.SetBool("Moving", true);
			sprite.flipX = false;
			transform.position = new Vector3(transform.position.x + runSpeed * Time.deltaTime, transform.position.y, transform.position.z);
		}
		else if (inputMgr.GetLeft() && !cannotRunLeft)
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			if (animator != null) animator.SetFloat("runSpeed", runSpeed);
			if (animator != null) animator.SetBool("Moving", true);
			sprite.flipX = true;
			transform.position = new Vector3(transform.position.x - runSpeed * Time.deltaTime, transform.position.y, transform.position.z);
		}
		else
		{
			audioSrc.Stop();
			if (animator != null) animator.SetFloat("runSpeed",0f);
            if (animator != null) animator.SetBool("Moving", false);
		}
	
	}
}
