using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    public string itemName;
    public void Interact()
    {
        Debug.Log($"Picked up Item with name: {itemName}");
        Destroy(gameObject);
    }
}
