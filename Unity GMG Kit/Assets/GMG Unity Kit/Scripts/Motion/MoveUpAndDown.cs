using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour {

	public float distanceToMoveOneWay = 2f;
	public float speed = 1f;
	public bool startUp = true;

	bool goingUp = true;
	Vector3 origPosition;
	// Use this for initialization
	float minY;
	float maxY;
	void Start () {
		goingUp = startUp;
		origPosition = transform.position;
		minY = origPosition.y - distanceToMoveOneWay;
		maxY = origPosition.y + distanceToMoveOneWay;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 currPos = transform.position;
		if (goingUp)
		{
			if (currPos.y >= maxY)
			{
				goingUp = false;
				transform.position = new Vector3(currPos.x, maxY, currPos.z);
			}
		}
		else
		{
			if (currPos.y <= minY)
			{
				goingUp = true;
				transform.position = new Vector3(currPos.x, minY, currPos.z);
			}			
		}

		if (goingUp)
		{
			transform.position = new Vector3(currPos.x, currPos.y + (speed * Time.deltaTime), currPos.z);
		}
		else
		{
			transform.position = new Vector3(currPos.x, currPos.y - (speed * Time.deltaTime), currPos.z);
		}		
	}
}
