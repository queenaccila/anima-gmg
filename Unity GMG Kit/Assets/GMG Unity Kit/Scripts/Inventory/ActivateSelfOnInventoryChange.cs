using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSelfOnInventoryChange : MonoBehaviour {

	public string itemName = "";
	public int quantity = 0;


	void OnInventoryChange(string _itemName, int _quantity)
	{
		if (itemName.ToLower() == _itemName && quantity == _quantity) activatable.Activate();
	}

	Activatable activatable;
	InventoryManager inventory;
	void Start()
	{
		activatable = gameObject.GetComponent<Activatable>();
		if (activatable == null) Debug.LogError("No OnActivation/OnDeactivation scripts added");
		inventory = GameManager.Inst().GetComponent<InventoryManager>() as InventoryManager;

		if (inventory != null) inventory.onInventoryChange += OnInventoryChange;
		else Debug.LogError("InventoryManager not attached to GameManager");
	}

	void OnDestroy()
	{
		if (inventory != null) inventory.onInventoryChange -= OnInventoryChange;
	}

	// Update is called once per frame
	void Update () 
	{
	}
}
