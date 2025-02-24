using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton instance

    private List<string> inventory = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log($"Added {itemName} to inventory. Total items: {inventory.Count}");
    }

    public void ShowInventory()
    {
        if (inventory.Count == 0)
        {
            Debug.Log("Inventory is empty.");
            return;
        }

        Debug.Log("Inventory Contents:");
        foreach (var item in inventory)
        {
            Debug.Log("- " + item);
        }
    }
}
