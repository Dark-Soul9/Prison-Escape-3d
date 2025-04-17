using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public static WeaponInventory Instance; // Singleton instance
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

    public List<WeaponSlot> allSlots = new List<WeaponSlot>();
    [SerializeField] private List<WeaponSlot> ownedWeapons = new List<WeaponSlot>();
    private int currentIndex = 0;

    public void AddWeapon(WeaponSlotType type, WeaponBehavior weapon)
    {
        var existing = allSlots.Find(slot => slot.slotType == type);
        if (existing != null && existing.weapon == null)
        {
            existing.weapon = weapon;
            ownedWeapons.Add(existing);
            UpdateHotkeys();
        }
    }

    private void UpdateHotkeys()
    {
        // Map 1 to ownedWeapons[0], 2 to [1], etc.
    }

    public void SwitchWeaponByScroll(bool scrollUp)
    {
        if (ownedWeapons.Count <= 1) return;

        currentIndex = scrollUp
            ? (currentIndex - 1 + ownedWeapons.Count) % ownedWeapons.Count
            : (currentIndex + 1) % ownedWeapons.Count;

        EquipWeapon(ownedWeapons[currentIndex]);
    }

    public void SwitchWeaponByNumber(int number)
    {
        int index = number - 1;
        if (index >= 0 && index < ownedWeapons.Count)
        {
            currentIndex = index;
            EquipWeapon(ownedWeapons[index]);
        }
    }

    private void EquipWeapon(WeaponSlot slot)
    {
        // Deactivate all, activate current
        foreach (var s in allSlots)
        {
            if (s.weapon != null)
                s.weapon.gameObject.SetActive(false);
        }

        if (slot.weapon != null)
        {
            slot.weapon.gameObject.SetActive(true);
        }
    }
}
