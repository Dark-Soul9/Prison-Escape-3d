using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    public Transform weaponHolder; // Assign this to the player's hand in Unity
    private GameObject currentWeapon;
    private Weapon currentWeaponScript;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon); // Remove old weapon
        
        currentWeapon = Instantiate(weaponPrefab, weaponHolder);
        currentWeaponScript = currentWeapon.GetComponent<Weapon>();

        ApplyWeaponOffsets();

        Debug.Log($"Equipped: {currentWeaponScript.weaponName}");
    }

    public void FireWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.GetComponent<Weapon>().Fire();
        }
    }
    private void ApplyWeaponOffsets()
    {
        if (currentWeaponScript == null) return;

        // Apply offsets
        currentWeapon.transform.localPosition = currentWeaponScript.positionOffset;
        currentWeapon.transform.localRotation = Quaternion.Euler(currentWeaponScript.rotationOffset);
        currentWeapon.transform.localScale = currentWeaponScript.scaleOffset;
    }
}
