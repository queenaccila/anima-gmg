using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

	bool activated = false;
	public delegate void ActivationHandler(bool activated);
	public event ActivationHandler onActivate;

	public void Activate()
	{
		if (gameMgr.isPaused) return;
		activated = !activated;
		if (onActivate != null) onActivate(activated);
	}

	// Use this for initialization
	GameManager gameMgr;
	void Start ()
	{
		gameMgr = GameManager.Inst();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
