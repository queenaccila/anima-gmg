using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerAnimManager : MonoBehaviour
{

	public AnimationClip runClip;
	public AnimationClip idleClip;
	public AnimationClip jumpClip;
	public AnimationClip fallClip;
	public AnimationClip deathClip;
	public AnimationClip shootClip;
	public AnimationClip crouchClip;
	public AnimationClip meleeClip;

	// Use this for initialization
	protected Animator animator;
	protected AnimatorOverrideController animatorOverrideController;
	void Awake()
	{
		animator = GetComponent<Animator>();
		if (animator == null)
		{
			animator = gameObject.AddComponent<Animator>();
		}

		animatorOverrideController = new AnimatorOverrideController(Resources.Load("Controllers/2DSideScroller/2DSidescrollerBlueprint") as RuntimeAnimatorController);
		animator.runtimeAnimatorController = animatorOverrideController;
		if (runClip != null) animatorOverrideController["Walk"] = runClip;
		if (idleClip != null) animatorOverrideController["Idle"] = idleClip;
		if (jumpClip != null) animatorOverrideController["Jump"] = jumpClip;
		if (fallClip != null) animatorOverrideController["Fall"] = fallClip;
		if (deathClip != null) animatorOverrideController["Death"] = deathClip;
		if (shootClip != null) animatorOverrideController["Shoot"] = shootClip;
		if (crouchClip != null) animatorOverrideController["Crouch"] = crouchClip;
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
}
