using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public Dictionary<string, int> Inventory = new Dictionary<string, int>();

	public void AddInventory(string itemName, int quantity)
	{
		if (Inventory.ContainsKey(itemName.ToLower()))
		{
			Inventory[itemName.ToLower()] += quantity;
		}
		else
		{
			Inventory[itemName.ToLower()] = quantity;
		}
	}

	public void RemoveInventory(string itemName, int quantity)
	{
		if (Inventory.ContainsKey(itemName.ToLower()))
		{
			Inventory[itemName.ToLower()] -= quantity;
		}
		else
		{
			Debug.LogError("Inventory item doesn't exist");
		}
	}

	public void SetInventory(string itemName, int quantity)
	{
		Inventory[itemName.ToLower()] = quantity;
	}

	public delegate void InventoryChangeHandler(string itemName, int quantity);
    public event InventoryChangeHandler onInventoryChange;
}