using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActivationPlayDialogue : ActivatableBehaviour {

	// Use this for initialization
	public DialoguerDialogues DialogueName;
	DialogueManager dialogueMgr;
	void Start ()
	{
		dialogueMgr = GameManager.Inst().GetComponent<DialogueManager>();
		if (!dialogueMgr) Debug.LogError("Need Dialogue Manager Component on GameManager!!");
	}
	
	public override void onActivate(bool activated)
	{
		if (activated)
		{
			dialogueMgr.PlayDialogue(DialogueName);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
