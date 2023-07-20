using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCirclePattern : MonoBehaviour {

	public float radiusOfCircle = 2f;
	public float speed = 10f;
	public float startingAngle = 1f;
	public bool goClockwise = true;
	public Transform transformOfRadius;

	float currAngle = 0f;
	// Use this for initialization
	void Start () {
		currAngle = startingAngle;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (goClockwise) currAngle -= Time.deltaTime * speed;
		else currAngle += Time.deltaTime * speed;

		float currAngleRads = currAngle * Mathf.Deg2Rad;

		Vector3 rotatedVec = new Vector3(Mathf.Cos(currAngleRads), Mathf.Sin(currAngleRads), 0);
		rotatedVec = rotatedVec * radiusOfCircle;

		transform.position = rotatedVec + transformOfRadius.position;
	}
}
