using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaycastWeaponData: WeaponData
{
    public float range;
    public int numberOfBulletsPerShot = 1;
}

public class RaycastWeapon : Weapon {

    public RaycastWeaponData raycastData;

    void Awake()
    {
        data = raycastData;
    }

    protected override void LaunchProjectile()
    {
        base.LaunchProjectile();
        for(int i = 0; i < raycastData.numberOfBulletsPerShot; i++)
        {
            RaycastHit hit;
            Vector3 shootDirection = data.muzzle.forward + transform.right * Random.Range(-1f, 1f) * (1 - currentAccuracy * movementMultiplier * data.owner.GetAccuracyMultiplier()) / (currentAccuracy * movementMultiplier * data.owner.GetAccuracyMultiplier());
            if (shootDirection.magnitude > 0 && Physics.Raycast(data.muzzle.position, shootDirection, out hit, raycastData.range, ~data.ignoreLayers))
            {
                if (hit.collider.gameObject.layer != data.myPlayerLayer && hit.collider.gameObject.GetComponent<HitBox>())
                {
                    hit.collider.gameObject.GetComponent<HitBox>().Hit(data.damage);
                }
            }
            Debug.DrawLine(data.muzzle.position, data.muzzle.position + shootDirection * raycastData.range, Color.red);
        }        
    }
}
