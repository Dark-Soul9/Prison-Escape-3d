using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage = 10;

    [Header("Weapon Positioning Offsets")]
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public Vector3 scaleOffset = Vector3.one;

    public void Fire()
    {
        Debug.Log($"{weaponName} fired! Damage: {damage}");
        // We will add actual shooting mechanics later
    }
}
