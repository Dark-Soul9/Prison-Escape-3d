using StarterAssets;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public float walkBobSpeed = 10f;
    public float walkBobAmount = 0.05f;

    public float runBobSpeed = 14f;
    public float runBobAmount = 0.1f;

    public float sneakBobSpeed = 6f;
    public float sneakBobAmount = 0.03f;

    private float timer = 0;
    private Vector3 originalPosition;

    private FirstPersonController player;
    private PlayerStates playerState;

    void Start()
    {
        originalPosition = transform.localPosition;
        player = FindObjectOfType<FirstPersonController>(); // Get movement script
        playerState = player.GetCurrentState();
    }

    void Update()
    {
        if (player == null) return;

        float speed = player.GetCurrentSpeed(); // We will add this in PlayerMovement
        float bobSpeed, bobAmount;

        if (playerState.currentState == PlayerStates.States.sprinting)
        {
            bobSpeed = runBobSpeed;
            bobAmount = runBobAmount;
        }
        else if (playerState.currentState == PlayerStates.States.sneaking)
        {
            bobSpeed = sneakBobSpeed;
            bobAmount = sneakBobAmount;
        }
        else if (playerState.currentState == PlayerStates.States.walking)
        {
            bobSpeed = walkBobSpeed;
            bobAmount = walkBobAmount;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5f);
            return;
        }

        timer += Time.deltaTime * bobSpeed;
        float bobOffset = Mathf.Sin(timer) * bobAmount;

        transform.localPosition = originalPosition + new Vector3(0, bobOffset, 0);
    }
}
