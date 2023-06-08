using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class PlayerShootScript : MonoBehaviour
    {
        public event Action<GunSO> OnWeaponSwitched;

        public Camera PlayerCamera;
        private PlayerInput PlayerInput;
        private PlayerScript PlayerScript;
        private GunSO CurrentWeapon;
        private PlayerMovement PlayerMovement;

        public Transform BulletSpawn;
        public LayerMask ProjectileLayer;

        public GameObject DebugNode;
        private bool IsReloading;


        private int CurrentAmmoCount;



        private float StartOfShotDelay;
        private float StartOfReload;

        private void Awake()
        {
            PlayerInput = GetComponent<PlayerInput>();
            PlayerScript = GetComponent<PlayerScript>();
            PlayerMovement = GetComponent<PlayerMovement>();
        }
        private void Start()
        {
            SwitchWeapons(PlayerScript.CurrentWeapon);
        }

        private void Update()
        {
            Debug.DrawRay(BulletSpawn.position, (GetAimPoint() - BulletSpawn.position));
            if (PlayerInput.Shoot())
            {
                TryShooting();
            }
            if (PlayerInput.Reload())
            {
                
                if(CurrentAmmoCount < CurrentWeapon.MagazineSize && !IsReloading)
                {
                    StartReload();
                }
            }
            if (PlayerInput.Release())
            {
                if (!IsReloading && !ShotDelayActive())
                {
                    StartOfShotDelay = Time.time;
                }
            }

            if (ShotDelayActive())
            {
                float elapsedTime = Time.time - StartOfShotDelay;
                float totalTime = CurrentWeapon.ShotDelay;
                GameManager.Instance.Hud.ShotDelaySlider.value = Mathf.Clamp01(elapsedTime / totalTime);
            }
            if (IsReloading)
            {
                float elapsedTime = Time.time - StartOfReload;
                float totalTime = CurrentWeapon.ReloadSpeed;
                GameManager.Instance.Hud.ReloadSlider.value = Mathf.Clamp01(elapsedTime / totalTime);
            }


        }
        private void TryShooting()
        {
            if (!IsReloading &&
                NotDown() &&
                CurrentAmmoCount > 0 &&
                !ShotDelayActive())
            {
                ShootWeapon();
            }
        }
        private bool NotDown()
        {
            return PlayerInput.HipFire() || PlayerInput.ADS();
        }

        private void ShootWeapon()
        {
            CurrentAmmoCount--;
            UpdateAmmoCounter();

            Vector3 TestForward = GetAimPoint();
            GameObject currentBullet = Instantiate(CurrentWeapon.Projectile, BulletSpawn.position, BulletSpawn.rotation);
            Projectile projectileScript = currentBullet.GetComponent<Projectile>();
            projectileScript.ParentWeapon = CurrentWeapon;
            projectileScript.ParentPlayer = gameObject;
            currentBullet.transform.forward = (GetAimPoint() - BulletSpawn.position).normalized;

            StartCoroutine(PlayerMovement.GunRecoil(CurrentWeapon.KickIntensity, CurrentWeapon.KickTime));
        }
        private Vector3 GetAimPoint()
        {
            int FLayerMask = LayerMask.NameToLayer("Projectile");
            int IGNORETHISFINGLAYER = ~(1 << FLayerMask);
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5F, 0.4F, 0)); //LOWERED VIEW

            return (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, IGNORETHISFINGLAYER)) ? hit.point : ray.GetPoint(300);
        }

        private void StartReload()
        {
            IsReloading = true;
            StartOfReload = Time.time;
            Debug.Log("Reloading...");
            Invoke("ReloadWeapon", CurrentWeapon.ReloadSpeed);
        }
        private void ReloadWeapon()
        {
            IsReloading = false;
            CurrentAmmoCount = CurrentWeapon.MagazineSize;
            StartOfShotDelay = Time.time - CurrentWeapon.ShotDelay;
            UpdateAmmoCounter();
            Debug.Log("Reloaded!");
        }
        private bool ShotDelayActive()
        {
            return StartOfShotDelay + CurrentWeapon.ShotDelay > Time.time;
        }
        private void UpdateAmmoCounter()
        {
            GameManager.Instance.Hud.AmmoText.text = $"{CurrentAmmoCount}";

            GameManager.Instance.Hud.ReloadSlider.value = Mathf.Clamp01((float)CurrentAmmoCount / (float)CurrentWeapon.MagazineSize);
        }

        public void SwitchWeapons(GunSO NewWeapon)
        {
            CurrentWeapon = NewWeapon;
            CurrentAmmoCount = NewWeapon.MagazineSize;
            OnWeaponSwitched(NewWeapon);
        }
    }
}
