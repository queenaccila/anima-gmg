using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownRun : MonoBehaviour {

	InputManager inputMgr;
	GameManager gameMgr;
	Animator animator;
	SpriteRenderer sprite;
	public float runSpeed = 0.1f;
	public bool cannotRunLeft = false;
	public bool cannotRunRight = false;
	public bool cannotRunUp = false;
	public bool cannotRunDown = false;

	public AudioClip soundFile;
	public bool LoopSound = true;
	AudioSource audioSrc;
	void Start ()
	{
		audioSrc = gameObject.AddComponent<AudioSource>();
		audioSrc.clip = soundFile;
		audioSrc.loop = LoopSound;
		animator = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
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
	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameMgr.isPaused) return;
	
		if (inputMgr.GetRight() && !cannotRunRight)
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			SetFace(Direction.Right);
			transform.position = new Vector3(transform.position.x + runSpeed, transform.position.y, transform.position.z);
		}
		else if (inputMgr.GetLeft() && !cannotRunLeft)
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			SetFace(Direction.Left);
			transform.position = new Vector3(transform.position.x - runSpeed, transform.position.y, transform.position.z);
		}
		else if (inputMgr.GetUp() && !cannotRunUp)
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			SetFace(Direction.Up);
			transform.position = new Vector3(transform.position.x, transform.position.y + runSpeed, transform.position.z);
		}
		else if (inputMgr.GetDown() && !cannotRunDown)
		{
			if (!audioSrc.isPlaying) audioSrc.Play();
			SetFace(Direction.Down);
			transform.position = new Vector3(transform.position.x, transform.position.y - runSpeed, transform.position.z);
		}		
		else
		{
			audioSrc.Stop();
			if (animator != null) animator.SetFloat("runSpeed",0f);
		}
	}

	void SetFace(Direction direction)
	{
		if (direction == Direction.Left)sprite.flipX = true;
		else sprite.flipX = false;
		
		if (animator == null) return;
		animator.SetFloat("runSpeed", runSpeed);
		animator.SetBool("FacingLeft", false);
		animator.SetBool("FacingRight", false);
		animator.SetBool("FacingUp", false);
		animator.SetBool("FacingDown", false);

		if (direction == Direction.Right)animator.SetBool("FacingRight", true);
		if (direction == Direction.Up)animator.SetBool("FacingUp", true);
		if (direction == Direction.Down)animator.SetBool("FacingDown", true);
		if (direction == Direction.Left)animator.SetBool("FacingLeft", true);
	}
}
