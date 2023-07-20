using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInDisplayFadeOut : MonoBehaviour {

	private enum scenestate : int {
		fadein,
		display,
		fadeout,
		none
	};
	scenestate state = scenestate.fadein;
	float fadetimer = 1f;
	float fadeValue = 0;

	public float displayTime = 3f;
	float displayTimer = 0f;
	public float intialDelayTime = 1.5f;
	float pausetimer = 0f;

	// Use this for initialization
	void Start () {
		foreach (Transform child in this.transform)
		{
			//child is your child transform
			if(child.GetComponent<Renderer>())
				child.GetComponent<Renderer>().material.color = new Color(1,1,1, fadeValue);
		}
		pausetimer = intialDelayTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(pausetimer>0)
		{
			pausetimer-=Time.deltaTime;
			if(pausetimer > 0)
				return;
		}

		if(state == scenestate.fadein)
		{
			if(fadetimer > 0)
			{
				fadetimer -=  Time.deltaTime;
				fadeValue = Mathf.Max(0,(1 - (fadetimer / 1f)));	
				foreach (Transform child in this.transform)
				{
					if(child.GetComponent<Renderer>())
						child.GetComponent<Renderer>().material.color = new Color(1,1,1, fadeValue);
				}
			}
			else
			{
				state = scenestate.display;
				displayTimer = displayTime;
			}
		}
		else if(state == scenestate.display)
		{
			displayTimer -= Time.deltaTime;
			if (displayTimer <= 0) {
				fadeValue = 0;
				fadetimer = 1f;	
				state = scenestate.fadeout;
			}
		}
		else if(state == scenestate.fadeout)
		{
			if(fadetimer > 0)
			{
				fadetimer -=  Time.deltaTime;
				fadeValue = Mathf.Max(0,(fadetimer / 1f));	
				foreach (Transform child in this.transform)
				{
					if(child.GetComponent<Renderer>())
						child.GetComponent<Renderer>().material.color = new Color(1,1,1, fadeValue);
				}
			}
			else
			{
				this.gameObject.SetActive (false);
				state = scenestate.none;
			}
		}

	}
}
