using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShootProjectile : MonoBehaviour {

	public GameObject projectilePrefab;
	public InputManager.InputButton shootButton = InputManager.InputButton.Action1;
	public float projectilesPerSecond = 5f;
	public float projectileSpeed = 1f;
	//Note make public in next release
	public bool fireInDirectionOfMouseCursor = false;

	InputManager inputMgr;
	GameManager gameMgr;
	Animator animator;
	SpriteRenderer sprite;
	// Use this for initialization
	public AudioClip soundFile;
	public bool LoopSound = false;
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
	
	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}

	void Shoot()
	{
		GameObject projectile = null;
		if (projectilePrefab != null) projectile = Instantiate(projectilePrefab);

		SpriteRenderer projSprite = null;
		if (projectile != null)
		{
			projectile.transform.position = transform.position;
			projSprite = projectile.GetComponent<SpriteRenderer>();
			
			
			ConstantForce2D forceComponent = projectile.AddComponent<ConstantForce2D>();

			projSprite.flipX = sprite.flipX;
			if (fireInDirectionOfMouseCursor)
			{
				Transform targetTransform = projectile.transform;

				Vector3 vectorToTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) - targetTransform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;

				float tempAngle = angle + 270;

				if      (tempAngle >= 135 && tempAngle < 225) SetFace(Direction.Right);
				else if (tempAngle >= 225 && tempAngle < 315) SetFace(Direction.Up);
				else if (tempAngle >=  45 && tempAngle <  135) SetFace(Direction.Down);
				else SetFace(Direction.Left);
				

				targetTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
							
				forceComponent.force = new Vector2(vectorToTarget.normalized.x * projectileSpeed, 
				vectorToTarget.normalized.y * projectileSpeed);
			}
			else
			{
				if (animator != null)
				{
					if (animator.GetBool("FacingUp")) forceComponent.force = new Vector2(0f, projectileSpeed);
					else if (animator.GetBool("FacingDown")) forceComponent.force = new Vector2(0f, -1*projectileSpeed);
					else forceComponent.force = new Vector2((projSprite.flipX) ? -1*projectileSpeed : projectileSpeed, 0f);
				}
				else forceComponent.force = new Vector2((projSprite.flipX) ? -1*projectileSpeed : projectileSpeed, 0f);
			}

			Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			if (!audioSrc.isPlaying) audioSrc.Play();
		}
	}

	public void OnPause(bool pause)
	{
		if (pause) audioSrc.Stop();
	}	
	void SetFace(Direction direction)
	{
		if (direction == Direction.Left)sprite.flipX = true;
		else if (direction == Direction.Right) sprite.flipX = false;
		
		if (animator == null) return;
		animator.SetBool("FacingLeft", false);
		animator.SetBool("FacingRight", false);
		animator.SetBool("FacingUp", false);
		animator.SetBool("FacingDown", false);

		if (direction == Direction.Right)animator.SetBool("FacingRight", true);
		if (direction == Direction.Up)animator.SetBool("FacingUp", true);
		if (direction == Direction.Down)animator.SetBool("FacingDown", true);
		if (direction == Direction.Left)animator.SetBool("FacingLeft", true);
	}	
	// Update is called once per frame
	float fireRateCounter = 0f;
	void Update () 
	{
		if (gameMgr.isPaused) return;
		
		if (fireRateCounter > (1f / projectilesPerSecond))
		{
			fireRateCounter = 0f;
		}
		else if (fireRateCounter != 0f)
		{
			fireRateCounter += Time.deltaTime;
		}

		if (inputMgr.GetKeyUp(shootButton) && animator != null)
		{
			animator.SetBool("isShooting", false);
			audioSrc.Stop();
		}

		if (inputMgr.GetKeyDown(shootButton) && animator != null) animator.SetBool("isShooting", true);

		if (inputMgr.GetKey(shootButton))
		{
			if (animator != null) animator.SetBool("isShooting", true);

			if (fireRateCounter == 0f)
			{
				Shoot();
				fireRateCounter += Time.deltaTime;
			}
		}

	}
}
