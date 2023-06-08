using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSO : ScriptableObject
{
    public GameObject Projectile;

    [Header("Stats")]
    public float BulletSpeed;
    public int MagazineSize;
    public float ReloadSpeed;
    public float ShotDelay;

    [Header("Mechanics")]
    public Vector2 SwaySpeed;
    public Vector2 SwayIntensity;
    public float KickTime;
    public float KickIntensity;

    [Header("Model")]
    public string WeaponName;
    public Texture IronSightsImage;

}
