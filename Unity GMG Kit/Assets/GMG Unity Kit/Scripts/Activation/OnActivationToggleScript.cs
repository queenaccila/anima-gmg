using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActivationToggleScript : ActivatableBehaviour
{
    public GameObject TargetGameObject;
    public string ScriptToToggle;

    public override void onActivate(bool activated)
    {
        if (activated)
        {
            Component script = TargetGameObject.GetComponent(ScriptToToggle);
            if (script == null)
            {
                Debug.LogError(ScriptToToggle + " script not found on specified game object");
            }
            else
            {
                (TargetGameObject.GetComponent(ScriptToToggle) as MonoBehaviour).enabled =
                !(TargetGameObject.GetComponent(ScriptToToggle) as MonoBehaviour).enabled;
            }
        }
    }

    void Update()
    {
        
    }
}
