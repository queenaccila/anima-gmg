using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActivationChangeScene : ActivatableBehaviour {

	// Use this for initialization
	public string SceneName;

	public override void onActivate(bool activated)
	{
		if (activated)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
