using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour {

	public float distanceToMoveOneWay = 2f;
	public float speed = 1f;
	public bool startRight = true;

	bool goingRight = true;
	Vector3 origPosition;
	// Use this for initialization
	float minX;
	float maxX;
	void Start () {
		goingRight = startRight;
		origPosition = transform.position;
		minX = origPosition.x - distanceToMoveOneWay;
		maxX = origPosition.x + distanceToMoveOneWay;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 currPos = transform.position;
		if (goingRight)
		{
			if (currPos.x >= maxX)
			{
				goingRight = false;
				transform.position = new Vector3(maxX, currPos.y, currPos.z);
			}
		}
		else
		{
			if (currPos.x <= minX)
			{
				goingRight = true;
				transform.position = new Vector3(minX, currPos.y, currPos.z);
			}			
		}

		if (goingRight)
		{
			transform.position = new Vector3(currPos.x + (speed * Time.deltaTime), currPos.y, currPos.z);
		}
		else
		{
			transform.position = new Vector3(currPos.x - (speed * Time.deltaTime), currPos.y, currPos.z);
		}		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
