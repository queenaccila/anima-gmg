using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneOnCollisionWithTag : MonoBehaviour {

	public string SceneName;
	public string collisionTag;
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.tag != collisionTag) return;
		UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
	}
    void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag != collisionTag) return;
		UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
