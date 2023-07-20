using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    public static bool IsDebugMode = false;
    public bool GenerationMode = false;

    public float MasterVolume = 1f;
    public float MusicVolume = 0.8f;
    public float SfxVolume = 1f;
}

public class GameManager : MonoBehaviour {

	private static GameManager gameMgr;
    public GameSettings Settings{ get; private set; }

	public static GameManager Inst()
	{
		if (gameMgr != null) return gameMgr;

		GameManager[] gameMgrs = Object.FindObjectsOfType(typeof(GameManager)) as GameManager[];
		foreach (GameManager gameManager in gameMgrs)
		{
			gameMgr = gameManager;
		}

		if (gameMgr == null)
		{
			GameObject gObject = Instantiate(Resources.Load("GameManager")) as GameObject;
			gameMgr = gObject.GetComponent<GameManager>();
		}

		if (gameMgr == null)
		{
			GameObject gameObj = new GameObject();
			gameMgr = gameObj.AddComponent<GameManager>();
		}

		return gameMgr;
	}


    public bool isPaused = false;
    public delegate void PauseHandler(bool pause);
    public event PauseHandler onPause;
    public void PauseGame() 		{isPaused = true; Time.timeScale = 0; if (onPause != null) onPause(true);}
    public void UnpauseGame() 	{isPaused = false; Time.timeScale = 1; if (onPause != null) onPause(false);}

    //Getters
    	InputManager inputMgr;
    	public InputManager InputManager()
    	{
    		if (inputMgr != null) return inputMgr;
    		inputMgr = GetComponent<InputManager>();

    		if (inputMgr == null)
    		{
    			inputMgr = gameObject.AddComponent<InputManager>();
    		}

    		return inputMgr;
    	}

		DialogueManager dialogueMgr;
    	public DialogueManager DialogueManager()
    	{
    		if (dialogueMgr != null) return dialogueMgr;
    		dialogueMgr = GetComponent<DialogueManager>();

    		if (dialogueMgr == null)
    		{
    			dialogueMgr = gameObject.AddComponent<DialogueManager>();
    		}
    		
    		return dialogueMgr;
    	}
    	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
