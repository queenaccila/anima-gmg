using UnityEngine;
using System.Collections;

public class GrowAndShrink : MonoBehaviour {

	public enum juicystate : int {
		normal, 
		grow, 
		shrink, 
	};
	juicystate state = juicystate.normal;
	public bool moving = false;

	public float growto = 1.1f;
	public float shrinkto = 1f;
	public float rate = 7f;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//print ("GrowAndShrink" + state);
		if(state == juicystate.normal)
		{
		}
		else if(state == juicystate.grow)
		{
			Vector3 scale  = this.transform.localScale;
			float newscale = Mathf.Min(growto,scale.x + Time.unscaledDeltaTime*rate);
			scale.x = newscale;
			scale.y = newscale;
			scale.z = newscale;
			this.transform.localScale = scale;
			if(newscale == growto)state =  juicystate.shrink;
		}
		else if(state == juicystate.shrink)
		{
			Vector3 scale  = this.transform.localScale;
			float newscale = Mathf.Max(shrinkto,scale.x - Time.unscaledDeltaTime*rate);
			scale.x = newscale;
			scale.y = newscale;
			scale.z = newscale;
			this.transform.localScale = scale;
			if(newscale == shrinkto)
			{	
				moving=false;
				//print ("moving = false;");
				state =  juicystate.normal;
			}
		}

	}
	
	public void StartEffect()
	{
		//print ("moving = true;");
		state = juicystate.grow;
		moving = true;
	}
	
}