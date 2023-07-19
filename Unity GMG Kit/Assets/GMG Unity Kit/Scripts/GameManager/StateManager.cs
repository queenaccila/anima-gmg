using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	public Dictionary<string, bool> State = new Dictionary<string, bool>();

	public void SetState(string stateName, bool value)
	{
		State[stateName.ToLower()] = value;
	}

	public delegate void StateChangeHandler(string stateName, bool value);
    public event StateChangeHandler onStateChange;
}
