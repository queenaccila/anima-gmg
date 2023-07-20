using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSceneOnCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		string SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
	}
    void OnTriggerEnter2D(Collider2D coll)
	{
		string SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
