using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSelfOnKeyPress : MonoBehaviour
{
    public InputManager.InputButton activateButton = InputManager.InputButton.Action1;
    Activatable activatable;
    InputManager inputMgr;

    void Start()
    {
        activatable = gameObject.GetComponent<Activatable>();
        inputMgr = GameManager.Inst().GetComponent<InputManager>(); 
    }

    void Activate()
    {
    	activatable.Activate();
    }

    void Update()
    {
        if (inputMgr.GetKeyDown(activateButton)) Activate();
    }
}
