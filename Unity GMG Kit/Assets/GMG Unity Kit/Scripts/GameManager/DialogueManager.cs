using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour 
{


	public bool PauseGameWhenPlayingDialogue = true;
    public InputManager.InputButton continueButton = InputManager.InputButton.Space;
    public InputManager.InputButton escapeButton = InputManager.InputButton.Escape;
    public AudioClip TextSound;

	public void PlayDialogue(DialoguerDialogues dialogue)
	{
		if (PauseGameWhenPlayingDialogue) gameMgr.PauseGame();
       	dialogueGUI.DialogueCanvas.SetActive(true);
		dialogueGUI.PlayDialogue(dialogue);
	}

	public void OnBeginDialogue()
	{
	}

	public void OnEndDialogue()
	{
		gameMgr.UnpauseGame();
	       	dialogueGUI.DialogueCanvas.SetActive(false);
	}

    public void OnSendMessage(string message, string metadata)
    {
    	//TODO CUTSCENES
        //EvaluateDialoguerMessage(message, metadata);
    }

	GameManager gameMgr;
	DialogueGUI dialogueGUI;
	GameObject dialogueObj;
	//MonoBehaviours
		void Start ()
		{
	       	gameMgr = GameManager.Inst();
	       	dialogueObj = Instantiate(Resources.Load("UI/Dialogue")) as GameObject;
	       	dialogueObj.transform.parent = gameObject.transform;

	       	//Setup
	       		dialogueGUI = dialogueObj.GetComponent<DialogueGUI>();
	       		dialogueGUI.continueButton = continueButton;
	       		dialogueGUI.escapeButton = escapeButton;
	       		dialogueGUI.textSound = TextSound;

	       	dialogueGUI.DialogueCanvas.SetActive(false);
		}
			
		// Update is called once per frame
		void Update ()
		{

		}
}
