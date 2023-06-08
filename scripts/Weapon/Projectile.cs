using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask ProjectileLayer;
    public float Lifetime;
    public GameObject hitEffect;

    public GunSO ParentWeapon;
    public GameObject ParentPlayer;

    private Vector3 LastPosition;


    private void Start()
    {
        LastPosition = transform.position;
        Invoke("MissShot", Lifetime);
    }

    private void FixedUpdate()
    {
        //move bullet
        transform.Translate(ParentWeapon.BulletSpeed * Time.fixedDeltaTime * Vector3.forward);

        Vector3 deltaPosition = transform.position - LastPosition;
        if (Physics.Raycast(LastPosition, deltaPosition.normalized, out RaycastHit hit, deltaPosition.magnitude, ~ProjectileLayer))
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, hit.point, Quaternion.identity);
            }
            if (hit.collider.CompareTag("Target"))
            {
                hit.collider.GetComponentInParent<TargetScripts.TargetScript>().OnHit();
                Destroy(gameObject);
            }
            else
            {
                MissShot();
            }


        }

        LastPosition = transform.position;
    }
    private void MissShot()
    {
        DrillManager.Instance.TargetMiss();
        Destroy(gameObject);
    }
}
