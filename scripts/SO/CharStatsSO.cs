using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharStatsSO : ScriptableObject
{
    [Header("Movement")]
    public float BaseMoveSpeed;
    public float SprintSpeed;
    public float JumpHeight;
    public float Gravity = -9.81f;
    [Header("Sensitivity")]
    public float LoweredSens;
    public float HipSens;
    public float ADSSens;
    public float ZoomSpeed = 5;

    [Header("Base FOV Levels")]
    public float LoweredFOV = 100;
    public float HipFOV = 90;
    public float ADSFOV = 40;
}
