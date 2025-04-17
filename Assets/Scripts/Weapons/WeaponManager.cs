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
        //if (currentWeapon != null)
            //Destroy(currentWeapon); // Remove old weapon
        
        currentWeapon = Instantiate(weaponPrefab, weaponHolder);
        currentWeaponScript = currentWeapon.GetComponent<WeaponBehavior>();
        currentWeaponScript.WeaponData();
        currentWeaponScript.ApplyWeaponOffsets();
        WeaponInventory.Instance.AddWeapon(currentWeaponScript.weaponData.weaponType, currentWeaponScript);
    }

    public void HandleWeaponInput(bool isFiring)
    {
        if (currentWeapon != null)
        {
            currentWeapon.GetComponent<WeaponBehavior>().HandleWeaponInput(isFiring);
        }
    }
    public void HandleAim(bool input)
    {
        if(currentWeapon != null)
        {
            currentWeapon.GetComponent<WeaponBehavior>().HandleWeaponAiming(input);
        }
    }
    public void HandleWeaponReload()
    {
        if(currentWeapon != null)
        {
            currentWeapon.GetComponent<WeaponBehavior>().Reload();
        }
    }
    
}
