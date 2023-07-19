using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneOnCollision : MonoBehaviour {

	public string SceneName;
	void OnCollisionEnter2D(Collision2D coll)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
	}
    void OnTriggerEnter2D(Collider2D coll)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
