using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGameObjectOnSceneLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
