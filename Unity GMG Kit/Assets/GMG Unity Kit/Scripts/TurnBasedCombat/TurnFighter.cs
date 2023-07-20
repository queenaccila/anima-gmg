using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFighter : MonoBehaviour {

	public bool isActive = true;

	public string Name;

	public int maxHitPoints;
	public int currentHitPoints;
	public int maxSpecialPoints;
	public int currentSpecialPoints;

	public string[] attackName;
	public int[] attackStrength;
	public int[] attackSpecialPoints;

	public TextMesh HUDText;
	public Transform Highlight;
	public Transform specialPointsBar;
	public Transform CharacterSprite;


	// Use this for initialization
	void Start () {
		bool ignoreSpecialPoints = true;
		for (int x = 0; x < attackSpecialPoints.Length; x++) {
			if (attackSpecialPoints [x] > 0)
				ignoreSpecialPoints = false;
		}
		if (ignoreSpecialPoints && specialPointsBar)
			specialPointsBar.gameObject.SetActive (false);
	}

	public bool IgnoreSpecialPoints()
	{
		bool ignoreSpecialPoints = true;
		for (int x = 0; x < attackSpecialPoints.Length; x++) {
			if (attackSpecialPoints [x] > 0)
				ignoreSpecialPoints = false;
		}
		return ignoreSpecialPoints;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
