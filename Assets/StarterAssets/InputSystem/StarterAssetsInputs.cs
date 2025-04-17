using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool sneak;
        public bool interact;
        public bool inventory;
        public bool fire;
        public bool aim;
        public bool reload;
        public bool scrollWeapon;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (cursorInputForLook)
            {
                LookInput(context.ReadValue<Vector2>());
            }
        }
        public void OnScroll(InputAction.CallbackContext context)
        {
            float scrollValue = context.ReadValue<Vector2>().y;

            if (scrollValue > 0)
                ScrollInput(true);
            else if (scrollValue < 0)
                ScrollInput(false);
        }

        public void OnNumberKey(InputAction.CallbackContext context)
        {
            int keyPressed = Mathf.FloorToInt(context.ReadValue<float>()); // Assuming you map 1–5 keys as actions
            //inventory.SwitchWeaponByNumber(keyPressed);
        }


        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                JumpInput(true);
            else if (context.canceled)
                JumpInput(false);
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
                SprintInput(true);
            else if (context.canceled)
                SprintInput(false);
        }

        public void OnSneak(InputAction.CallbackContext context)
        {
            if (context.performed)
                SneakInput(true);
            else if (context.canceled)
                SneakInput(false);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                InteractInput(true);
            else if (context.canceled)
                InteractInput(false);
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (context.performed)
                InventoryInput(true);
            else if (context.canceled)
                InventoryInput(false);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
                FireInput(true);
            else if (context.canceled)
                FireInput(false);
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
                AimInput(true);
            else if (context.canceled)
                AimInput(false);
        }
        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.performed)
                ReloadInput(true);
            else if (context.canceled)
                ReloadInput(false);
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void SneakInput(bool newSneakState)
        {
            sneak = newSneakState;
        }

        public void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }

        public void InventoryInput(bool newInventoryState)
        {
            inventory = newInventoryState;
        }

        public void FireInput(bool newFireState)
        {
            fire = newFireState;
        }

        public void AimInput(bool newAimState)
        {
            aim = newAimState;
            
        }
        public void ReloadInput(bool newReloadState)
        {
            reload = newReloadState;
            Debug.Log("Reload Pressed: " + reload);
        }
        public void ScrollInput(bool newScrollState)
        {
            scrollWeapon = newScrollState;
            Debug.Log("Scroll is: " + scrollWeapon);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
