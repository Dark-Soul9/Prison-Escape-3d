using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    public string itemName;
    public void Interact()
    {
        InventoryManager.Instance.AddItem(itemName);
        Destroy(gameObject);
    }
}
