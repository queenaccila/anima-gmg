using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	public float maxHealth = 100f;
	float currentHealth = 100f;

	Animator animator;
	void Start()
	{
		animator = GetComponent<Animator>();
		ChangeHealth(maxHealth);
	}

	void Update ()
	{

	}

	void ChangeHealth(float amount)
	{
		if (dead) return;

		currentHealth = amount;
		if (onHealthManagerChange != null) onHealthManagerChange(currentHealth);		

		if (currentHealth <= 0f)
		{
			Die();
		}
		Debug.Log(gameObject.name + " has " + currentHealth +" Health");
	}

	public void Heal(float healAmount)
	{
		ChangeHealth(currentHealth + healAmount);
	}
	
	public void TakeDamage(float dmgAmount)
	{
		ChangeHealth(currentHealth - dmgAmount);
	}


	public delegate void HealthManagerChangeHandler(float currHealth);
	public event HealthManagerChangeHandler onHealthManagerChange;

	public delegate void HealthManagerDeathHandler();
	public event HealthManagerDeathHandler onHealthManagerDeath;
	bool dead = false;

	public void Kill() {Die();}
	
	void Die()
	{
		dead = true;
		currentHealth = 0f;
		if (onHealthManagerDeath != null) onHealthManagerDeath();
		if (animator != null) animator.SetBool("isDead", true);
	}
}