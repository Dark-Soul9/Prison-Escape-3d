using StarterAssets;
using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f; // How far the player can interact
    public LayerMask interactableLayer; // Assign "Interactable" layer in Unity

    private Camera playerCamera;
    private StarterAssetsInputs _input;
    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        playerCamera = Camera.main;
    }
    private void Update()
    {
        if(_input.interact)
        {
            TryInteract();
        }
    }
    void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }   
    }
}
