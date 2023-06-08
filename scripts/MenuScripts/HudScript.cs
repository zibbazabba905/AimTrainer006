using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerScripts;

namespace MenuScripts
{
    public class HudScript : MonoBehaviour
    {
        public Text TimeText;
        public Text AmmoText;
        public Text ScoreText;
        public GameObject Crosshair;
        public Slider ShotDelaySlider;
        public Slider ReloadSlider;
        public GameObject Ironsights;

        private Camera PlayerCamera;
        private PlayerInput PlayerInput;

        private void Awake()
        {
            LevelManager.Instance.OnPlayerSpawned += HandlePlayerSpawned;
        }

        private void HandlePlayerSpawned(PlayerScript player)
        {
            PlayerCamera = player.transform.Find("CameraRig/Camera").GetComponent<Camera>();
            PlayerInput = player.GetComponent<PlayerInput>();
            player.GetComponent<PlayerShootScript>().OnWeaponSwitched += ChangeIronSights;
            LowerViewlikeHunt();
        }
        private void LowerViewlikeHunt()
        {
            PlayerCamera.lensShift = new Vector2(0f, 0.085f);
            Crosshair.transform.position = new Vector3(Screen.width / 2, 432f, 0f);
        }
        private void Update()
        {
            Crosshair.SetActive(PlayerInput.HipFire());
            Ironsights.SetActive(PlayerInput.ADS());
        }
        
        public void ChangeIronSights(GunSO NewGun)
        {
            Ironsights.GetComponent<RawImage>().texture = NewGun.IronSightsImage;
        }

    }
}
