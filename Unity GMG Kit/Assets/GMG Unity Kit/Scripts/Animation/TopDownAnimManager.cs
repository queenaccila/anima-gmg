using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimManager : MonoBehaviour
{

	public AnimationClip walkClip;
	public AnimationClip walkUpClip;
	public AnimationClip walkDownClip;
	public AnimationClip idleClip;
	public AnimationClip idleUpClip;
	public AnimationClip idleDownClip;
	public AnimationClip deathClip;
	public AnimationClip deathUpClip;
	public AnimationClip deathDownClip;
	public AnimationClip shootClip;
	public AnimationClip shootUpClip;
	public AnimationClip shootDownClip;
	public AnimationClip meleeClip;
	public AnimationClip meleeUpClip;
	public AnimationClip meleeDownClip;

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

		animatorOverrideController = new AnimatorOverrideController(Resources.Load("Controllers/2DTopDown/2DTopDownBluePrint") as RuntimeAnimatorController);
		animator.runtimeAnimatorController = animatorOverrideController;
		if (walkClip != null) 		animatorOverrideController["Walk"] 		= walkClip;
		if (walkUpClip != null) 	animatorOverrideController["WalkUp"] 	= walkUpClip;
		if (walkDownClip != null) 	animatorOverrideController["WalkDown"] 	= walkDownClip;
		if (idleClip != null) 		animatorOverrideController["Idle"] 		= idleClip;
		if (idleUpClip != null) 	animatorOverrideController["IdleUp"] 	= idleUpClip;
		if (idleDownClip != null) 	animatorOverrideController["IdleDown"] 	= idleDownClip;
		if (deathClip != null) 		animatorOverrideController["Death"] 	= deathClip;
		if (deathUpClip != null) 	animatorOverrideController["DeathUp"] 	= deathUpClip;
		if (deathDownClip != null) 	animatorOverrideController["DeathDown"] = deathDownClip;
		if (shootClip != null) 		animatorOverrideController["Shoot"] 	= shootClip;
		if (shootUpClip != null) 	animatorOverrideController["ShootUp"] 	= shootUpClip;
		if (shootDownClip != null) 	animatorOverrideController["ShootDown"] = shootDownClip;
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
}
