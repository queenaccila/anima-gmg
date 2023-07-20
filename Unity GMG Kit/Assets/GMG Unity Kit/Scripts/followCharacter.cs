using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCharacter : MonoBehaviour {
	
	public float speed;

	private Transform target;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("book").GetComponent<Transform>();	
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("a")) {
			target = GameObject.FindGameObjectWithTag ("book").GetComponent<Transform> ();		
		}
		else if(Input.GetKeyDown("k") ){
			target = GameObject.FindGameObjectWithTag ("bomb").GetComponent<Transform> ();		
		}
		else if(Input.GetKeyDown("l") ){
			target = GameObject.FindGameObjectWithTag ("bucket").GetComponent<Transform> ();		
		}
		transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.maximumDeltaTime);
	}
}
