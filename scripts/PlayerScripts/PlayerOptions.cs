using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerOptions : MonoBehaviour
    {

        [field: SerializeField] public float LoweredSens { get; set; }
        [field: SerializeField] public float HipSens { get; set; }
        [field: SerializeField] public float AimSens { get; set; }
        [field: SerializeField] public bool GunslingerMode { get; set; }

        [field: SerializeField] public float LoweredFov { get; set; }
        [field: SerializeField] public float HipFov { get; set; }
        [field: SerializeField] public float AimFov { get; set; }
        [field: SerializeField] public GunSO CurrentWeapon { get; set; }
    }
}
