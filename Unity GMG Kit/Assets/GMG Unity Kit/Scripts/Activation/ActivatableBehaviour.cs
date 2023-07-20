using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableBehaviour : MonoBehaviour {


	protected bool isSetup = false;
    void Awake()
    {
        if (this.enabled && !isSetup) SetupActivatable();
    }

    void OnDestroy()
	{
		if (isSetup) TeardownActivatable();
	}

	void OnEnable()
	{
		if (!isSetup) SetupActivatable();
	}

	void OnDisable()
	{
		if (isSetup) TeardownActivatable();
	}

	protected void SetupActivatable()
	{
		Activatable activatable = GetComponent<Activatable>();
		if (activatable == null)
		{
			activatable = gameObject.AddComponent<Activatable>() as Activatable;
		}
		activatable.onActivate += onActivate;
		isSetup = true;
	}

	protected void TeardownActivatable()
	{
		Activatable activatable = GetComponent<Activatable>();
		if  (activatable != null) activatable.onActivate -= onActivate;
		isSetup = false;
	}

	public virtual void onActivate(bool activated){}
}
