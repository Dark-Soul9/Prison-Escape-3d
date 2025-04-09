using Cinemachine;
using UnityEngine;
using UnityEngine.Windows;

public class WeaponBehavior : MonoBehaviour
{
    [Header("Weapon Settings")]
    public WeaponData weaponData;

    [Header("References")]
    public Transform weaponMuzzle; // Point where bullets come out
    public CinemachineVirtualCamera virtualCamera;
    private float fireCooldown;
    private int currentAmmo;

    private bool isAiming = false;
    private float originalFOV;
    private bool canShoot = true;

    private void OnEnable()
    {
        virtualCamera = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            originalFOV = virtualCamera.m_Lens.FieldOfView;
            virtualCamera.m_Lens.FieldOfView = originalFOV; // Ensure it resets on weapon switch
        }
        isAiming = false;
    }
    private void OnDisable()
    {
        if (virtualCamera != null)
            virtualCamera.m_Lens.FieldOfView = originalFOV;
    }


    private void Start()
    {
        WeaponData();
        ApplyWeaponOffsets();
        currentAmmo = weaponData.magazineSize;
    }

    private void Update()
    {
        HandleAiming(isAiming);
        fireCooldown -= Time.deltaTime;
    }
    /*
    public void HandleWeaponInput()
    {
        // Firing
        if (weaponData.fireMode == FireMode.SemiAuto)
        {
            if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f && canShoot)
                Fire();
        }
        else if (weaponData.fireMode == FireMode.Automatic)
        {
            if (Input.GetButton("Fire1") && fireCooldown <= 0f && canShoot)
                Fire();
        }
    }
    */
    public void HandleWeaponAiming(bool aimInput)
    {
        isAiming = aimInput;
        Debug.Log("Input pressed is " + isAiming);
    }

    private void Fire()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        Debug.Log($"Fired {weaponData.weaponName}! Damage: {weaponData.damage}");

        currentAmmo--;
        fireCooldown = weaponData.fireRate;

        // You can later add effects here (muzzle flash, sounds, recoil)
    }

    private void HandleAiming(bool aimInput)
    {
        float targetFOV = (aimInput && weaponData.hasScope) ? weaponData.scopedFOV : originalFOV;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * weaponData.aimSpeed);
        /*
        if (aimInput && weaponData.hasScope)
        {
            virtualCamera.m_Lens.FieldOfView = weaponData.scopedFOV;
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = originalFOV;
        }
        */
    }


    public void Reload()
    {
        currentAmmo = weaponData.magazineSize;
        Debug.Log("Reloaded!");
    }
    public void WeaponData()
    {
        Debug.Log($"Equipped: {weaponData.weaponName}");
    }
    public void ApplyWeaponOffsets()
    {
        if (this == null) return;

        // Apply offsets
        this.transform.localPosition = weaponData.positionOffset;
        this.transform.localRotation = Quaternion.Euler(weaponData.rotationOffset);
        this.transform.localScale = weaponData.scaleOffset;
    }
}
