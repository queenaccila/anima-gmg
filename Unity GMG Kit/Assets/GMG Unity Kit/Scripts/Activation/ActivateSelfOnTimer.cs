using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSelfOnTimer : MonoBehaviour
{
    public int TimerLength = 0;
    public bool LoopTimer = true;

    Activatable activatable;
	void Start()
	{
		activatable = gameObject.GetComponent<Activatable>();
		if (activatable == null) Debug.LogError("No OnActivation/OnDeactivation scripts added");
	}

	float timerCounter = 0;
	void Update()
	{
		if (timerCounter >= TimerLength)
		{
			OnTimer();
			return;
		}
		timerCounter += Time.deltaTime;
	}
    
    bool TimerFinishedOnce = false;
    void OnTimer()
    {
    	if(LoopTimer || !TimerFinishedOnce) activatable.Activate();
    	TimerFinishedOnce = true;
    }
}
