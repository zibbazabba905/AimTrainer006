using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        public bool ControlModeGunslinger = false;


        private void Update()
        {
            if (Escape())
            {
                GameManager.Instance.TogglePause();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ControlModeGunslinger = !ControlModeGunslinger;
            }
        }




        public bool GameIsPaused()
        {
            return GameManager.Instance.IsPaused;
        }

        public float HorizontalInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }
        public float VerticalInput()
        {
            return Input.GetAxisRaw("Vertical");
        }
        public float MouseX()
        {
            return Input.GetAxisRaw("Mouse X");
        }
        public float MouseY()
        {
            return Input.GetAxisRaw("Mouse Y");
        }
        public bool Jump()
        {
            return Input.GetButtonDown("Jump");
        }
        public bool Sprint()
        {
            //if ads is false and such
            return Input.GetKey(KeyCode.LeftShift);
        }
        public bool ADS()
        {
            if (!Input.GetMouseButton(1))
                return false;
            if (ControlModeGunslinger)
                return true;
            if (Input.GetKey(KeyCode.LeftShift))
                return true;
            return false;
        }
        public bool HipFire()
        {
            if (Sprint())
                return false;
            if (!ControlModeGunslinger && !Input.GetMouseButton(1))
                return false;
            if (ADS())//FIX quickfix make this better later
                return false;
            return true;
        }
        public bool Shoot()
        {
            if (GameIsPaused())
                return false;
            //shooting inputs
            return Input.GetMouseButtonDown(0);
        }
        public bool Release()
        {
            if (GameIsPaused())
                return false;

            return Input.GetMouseButtonUp(0);
        }

        public bool Escape()
        {
            return Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape);
        }
        public bool Reload()
        {
            if (GameIsPaused())
                return false;

            return Input.GetKeyDown(KeyCode.R);
        }
    }
}
