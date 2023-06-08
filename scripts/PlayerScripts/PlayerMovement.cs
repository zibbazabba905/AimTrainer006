using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerScript PlayerScript;
        private PlayerInput PlayerInput;
        private CharacterController CharacterController;
        private PlayerShootScript PlayerShootScript;
        public CharStatsSO CharacterStats;// NOTE return
        private PlayerOptions PlayerOptions;
        public GameObject CameraRig;
        public Camera PlayerCamera;

        public int SensMultiplyer;

        private bool IsGrounded;

        private Vector3 Velocity;

        float CurrentFOV;
        float CurrentSens;

        private GunSO CurrentWeapon;

        void Awake()
        {
            PlayerScript = GetComponent<PlayerScript>();
            PlayerInput = GetComponent<PlayerInput>();



            CharacterController = GetComponent<CharacterController>();
            PlayerShootScript = GetComponent<PlayerShootScript>();

        }
        private void Start()
        {
            PlayerOptions = PlayerScript.PlayerOptions;
            CurrentFOV = PlayerOptions.LoweredFov;
            CurrentSens = PlayerOptions.LoweredSens;

            CurrentWeapon = GetComponent<PlayerScript>().CurrentWeapon;
        }



        void Update()
        {
            if (GameManager.Instance.IsPaused)
                return;

            HandleMovement();
            AimMode();
            HandleView();
        }

        private void HandleMovement()
        {
            IsGrounded = CharacterController.isGrounded;

            //get input
            float Horizontal = PlayerInput.HorizontalInput();
            float Vertical = PlayerInput.VerticalInput();
            float speed = (PlayerInput.Sprint() ? CharacterStats.SprintSpeed : CharacterStats.BaseMoveSpeed);

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            forward.y = 0f;
            right.y = 0f;

            // Calculate the movement direction based on the input and the character's forward direction
            Vector3 inputVelocity = speed * (forward * Vertical + right * Horizontal).normalized;

            // Apply gravity and inputs to the movement direction
            Velocity.x = inputVelocity.x;
            Velocity.y += CharacterStats.Gravity * Time.deltaTime;
            Velocity.z = inputVelocity.z;

            // Jump if the character is on the ground and the jump button is pressed
            if (IsGrounded && PlayerInput.Jump())
            {
                Velocity.y = Mathf.Sqrt(CharacterStats.JumpHeight * -2f * CharacterStats.Gravity);
            }

            // Move the character
            CharacterController.Move(Velocity * Time.deltaTime);
        }
        
        private void HandleView()
        {
            float mouseX = PlayerInput.MouseX() * CurrentSens  * SensMultiplyer * Time.deltaTime;
            float mouseY = PlayerInput.MouseY() * CurrentSens * SensMultiplyer * Time.deltaTime;

            float swayX = Mathf.Cos(Time.time * CurrentWeapon.SwaySpeed.x) * CurrentWeapon.SwayIntensity.x;
            float swayY = Mathf.Sin(Time.time * CurrentWeapon.SwaySpeed.y) * CurrentWeapon.SwayIntensity.y;

            transform.Rotate(Vector3.up * mouseX + Vector3.up * swayX);

            CameraRig.transform.Rotate(Vector3.right * -mouseY + Vector3.right * swayY);
            //do I really need this line?
            CameraRig.transform.localRotation = Quaternion.Euler(CameraRig.transform.localRotation.eulerAngles.x, 0f, 0f);

            PlayerCamera.fieldOfView = Camera.HorizontalToVerticalFieldOfView(CurrentFOV, 1.78f);

        }

        private void AimMode()
        {
            bool IsAiming = false;

            if (PlayerInput.ADS()) // Right mouse button
            {
                IsAiming = true;
                CurrentFOV = Mathf.Lerp(CurrentFOV, PlayerOptions.AimFov, Time.deltaTime * CharacterStats.ZoomSpeed);
                CurrentSens = PlayerOptions.AimSens;
            }

            if (!IsAiming)
            {
                if (PlayerInput.HipFire())
                {
                    //currentFOV = hipFireFOV;
                    CurrentFOV = Mathf.Lerp(CurrentFOV, PlayerOptions.HipFov, Time.deltaTime * CharacterStats.ZoomSpeed);
                    CurrentSens = PlayerOptions.HipSens;
                }
                else
                {
                    //currentFOV = loweredFOV;
                    CurrentFOV = Mathf.Lerp(CurrentFOV, PlayerOptions.LoweredFov, Time.deltaTime * CharacterStats.ZoomSpeed);
                    CurrentSens = PlayerOptions.LoweredSens;
                    //IsDown = true;
                }
            }
        }

        public IEnumerator GunRecoil(float recoilAmount, float duration)
        {
            float elapsed = 0f;
            Quaternion originalRotation = CameraRig.transform.localRotation;
            Quaternion recoilRotation = Quaternion.Euler(CameraRig.transform.localRotation.eulerAngles.x - recoilAmount, 0f, 0f);

            while (elapsed < duration)
            {
                CameraRig.transform.localRotation = Quaternion.Lerp(originalRotation, recoilRotation, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0f;
            while (elapsed < duration)
            {
                CameraRig.transform.localRotation = Quaternion.Lerp(recoilRotation, originalRotation, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            CameraRig.transform.localRotation = originalRotation;
        }

    }
}
