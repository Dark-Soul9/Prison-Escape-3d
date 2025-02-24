using UnityEngine;

public class WeaponPickup : MonoBehaviour, IInteractable
{
    public GameObject weaponPrefab; // Assign a weapon prefab in Unity

    public void Interact()
    {
        WeaponManager.Instance.EquipWeapon(weaponPrefab);
        Destroy(gameObject); // Remove weapon from world after picking up
    }
}
