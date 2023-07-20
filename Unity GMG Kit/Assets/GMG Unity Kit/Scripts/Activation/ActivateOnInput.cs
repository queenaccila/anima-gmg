using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnInput : MonoBehaviour {

	public InputManager.InputButton activateButton = InputManager.InputButton.Action1;

	bool activate = false;
	InputManager inputMgr;

	List<Activatable> activatables = new List<Activatable>();
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.gameObject == null) return;
		Activatable activatable = coll.collider.gameObject.GetComponent<Activatable>();
		if(activatable != null && !activatables.Contains(activatable)) activatables.Add(activatable);
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.collider.gameObject == null) return;
		Activatable activatable = coll.collider.gameObject.GetComponent<Activatable>();
		if(activatable != null && activatables.Contains(activatable)) activatables.Remove(activatable);
	}

	void Activate()
	{
		for (int i = 0; i < activatables.Count; i++)
		{
			activatables[i].Activate();
		}
	}

	void Start()
	{
		inputMgr = GameManager.Inst().GetComponent<InputManager>();		
	}

	// Update is called once per frame
	void Update () 
	{
		if (inputMgr.GetKeyDown(activateButton)) Activate();
	}
}
