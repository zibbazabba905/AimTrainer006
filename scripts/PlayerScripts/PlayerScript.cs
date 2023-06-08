using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerScript : MonoBehaviour
    {
        public GunSO CurrentWeapon;
        public PlayerOptions PlayerOptions;

        private void Start()
        {
            CurrentWeapon = PlayerOptions.CurrentWeapon;
        }
    }    
}
