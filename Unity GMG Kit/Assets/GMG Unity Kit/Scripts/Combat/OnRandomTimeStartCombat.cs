using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRandomTimeStartCombat : MonoBehaviour {

	public float minTime = 5f;
	public float maxTime = 10f;

	float randomtimer = 0f;

	Vector3 lastPosition;

	GameObject TurnCombatPrefab;
	public GameObject[] turnCombatEnemies;

	// Use this for initialization
	void Start () {
		TurnCombatPrefab = GameObject.Find ("TurnCombatPrefab");
		if (!TurnCombatPrefab)
			print ("MISSING TurnCombatPrefab");
		
		randomtimer = Random.Range (minTime, maxTime);
		lastPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (lastPosition != this.transform.position) {
			lastPosition = this.transform.position;
			randomtimer -= Time.deltaTime;

			if (randomtimer <= 0) {
				print ("Start Turn Combat");

				TurnCombatPrefab.GetComponent<TurnCombatManager> ().StartTurnCombat (turnCombatEnemies);
				randomtimer = Random.Range (minTime, maxTime);
			}
		}
	}
}
