using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionStartTurnCombat : MonoBehaviour {

	GameObject TurnCombatPrefab;
	public GameObject[] turnCombatEnemies;

	// Use this for initialization
	void Start () {

		TurnCombatPrefab = GameObject.Find ("TurnCombatPrefab");
		if (!TurnCombatPrefab)
			print ("MISSING TurnCombatPrefab");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (TurnCombatPrefab) {
			print ("Start Turn Combat");
			this.gameObject.SetActive (false);

			TurnCombatPrefab.GetComponent<TurnCombatManager> ().StartTurnCombat (turnCombatEnemies);
		}

	}

}
