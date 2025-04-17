using UnityEngine;

public enum FireMode
{
    SemiAuto,
    Automatic
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    public string weaponName = "New Weapon";
    public FireMode fireMode = FireMode.SemiAuto;
    public float fireRate = 0.2f; // Time between shots
    public float damage = 10f;
    public float range;
    public WeaponSlotType weaponType;

    [Header("Recoil")]
    public float recoilAmount = 1f;

    [Header("Aiming")]
    public bool hasScope = false;
    public float scopedFOV = 30f;
    public float aimSpeed = 10f;

    [Header("Visuals")]
    public GameObject weaponPrefab; // Assigned weapon model prefab

    [Header("Ammo")]
    public int magazineSize = 10;
    public float reloadTime = 1.5f;

    [Header("Weapon Positioning Offsets")]
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public Vector3 scaleOffset = Vector3.one;
}
