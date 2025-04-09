using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    public Transform weaponHolder; // Assign this to the player's hand in Unity
    private GameObject currentWeapon;
    private WeaponBehavior currentWeaponScript;

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
        currentWeaponScript = currentWeapon.GetComponent<WeaponBehavior>();
        currentWeaponScript.WeaponData();
        currentWeaponScript.ApplyWeaponOffsets();
    }

    public void HandleWeapon()
    {
        if (currentWeapon != null)
        {
            //currentWeapon.GetComponent<WeaponBehavior>().HandleWeaponInput();
            //currentWeapon.GetComponent<WeaponBehavior>().HandleWeaponInput();
        }
    }
    public void HandleAim(bool input)
    {
        if(currentWeapon != null)
        {
            currentWeapon.GetComponent<WeaponBehavior>().HandleWeaponAiming(input);
        }
    }
    
}
