using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeactivationSetInventory : ActivatableBehaviour
{
	public string ItemName = "";
	public int Quantity = 0;
    InventoryManager inventoryMgr;

    void Start()
    {
    	inventoryMgr = GameManager.Inst().GetComponent<InventoryManager>() as InventoryManager;
    	if (inventoryMgr == null) Debug.LogError("InventoryManager not attached to GameManager");
    }

    public override void onActivate(bool activated)
    {
        if (!activated)
        {
           inventoryMgr.SetInventory(ItemName, Quantity);
        }
    }
}
