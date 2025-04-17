using Cinemachine;
using System.Collections;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    [Header("Weapon Settings")]
    public WeaponData weaponData;

    [Header("References")]
    public Transform weaponMuzzle; // Point where bullets come out
    public CinemachineVirtualCamera virtualCamera;
    private float fireCooldown = 0f;
    private int currentAmmo;
    [SerializeField] private LayerMask damageableLayers;

    private bool isAiming = false;
    private float originalFOV;
    private bool canShoot = true;
    private bool isReloading = false;

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
        fireCooldown -= Time.deltaTime;
        HandleAiming(isAiming);
    }
    public void HandleWeaponInput(bool isFiring)
    {
        if (isReloading)
        {
            isFiring = false;
            return;
        }
        if (!canShoot || currentAmmo <= 0) return;

        if (weaponData.fireMode == FireMode.SemiAuto)
        {
            if (isFiring && fireCooldown <= 0f)
            {
                Fire();
            }
        }
        else if (weaponData.fireMode == FireMode.Automatic)
        {
            if (isFiring && fireCooldown <= 0f)
            {
                Fire();
            }
        }
    }
    public void HandleWeaponAiming(bool aimInput)
    {
        if (isReloading)
        {
            isAiming = false;
            return;
        }
        isAiming = aimInput;
        //Debug.Log("Input pressed is " + isAiming);
    }

    private void Fire()
    {
        currentAmmo--;
        fireCooldown = weaponData.fireRate;

        // Raycast to simulate bullet hit (hitscan style)
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hitInfo, weaponData.range))
        {
            Debug.Log($"Hit {hitInfo.collider.name}");

            // TODO: Add damage logic
            IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(weaponData.damage);
            }


            // TODO: Add impact effects
        }

        // TODO: Muzzle flash, audio, recoil, camera shake etc.

        Debug.Log($"Fired {weaponData.weaponName}. Ammo left: {currentAmmo}");
    }


    private void HandleAiming(bool aimInput)
    {
        float targetFOV = (aimInput && weaponData.hasScope) ? weaponData.scopedFOV : originalFOV;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * weaponData.aimSpeed);
    }


    public void Reload()
    {
        if (isReloading == false && currentAmmo < weaponData.magazineSize)
        {
            StartCoroutine(ReloadRoutine());
        }
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
    IEnumerator ReloadRoutine()
    {
        if(isReloading)
        {
            StopCoroutine(ReloadRoutine());
        }
        isReloading = true;
        yield return new WaitForSeconds(weaponData.reloadTime);
        isReloading = false;
        currentAmmo = weaponData.magazineSize;
        Debug.Log("Reloaded!");
    }
}
