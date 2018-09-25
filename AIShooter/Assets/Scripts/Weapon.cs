using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData 
{
    public string name = "GUN";
    public Player owner;
    public Transform muzzle;
    public float damage;
    public float fireRate;
    [Range(0.0f, 1.0f)]
    public float accuracy;
    [Range(0.0f, 1.0f)]
    public float accuracyMultiplierPerShot;
    [HideInInspector]
    public float accuracyMultiplier = 1;
    public int magazineSize;
    //[HideInInspector]
    public int myPlayerLayer;
    public LayerMask ignoreLayers;
    public float reloadTime;
    public float accuracyResetRate;
    public float movementAccuracyMultiplierBase = 10;
}

public class Weapon : MonoBehaviour {

    [HideInInspector]
    public WeaponData data;
    private bool firing = false;
    [SerializeField]
    private bool reloading = false;
    private int bulletsRemaining;
    protected float currentAccuracy;
    private float fireWaitTime = 0f;
    private Vector3 lastPosition;
    protected float movementMultiplier;

	// Use this for initialization
	protected virtual void Start ()
    {
        bulletsRemaining = data.magazineSize;
        ResetAccuracy();
        lastPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(fireWaitTime > 0)
        {
            fireWaitTime -= Time.deltaTime;
        }
		if (firing)
        {
            if (bulletsRemaining > 0)
            {
                if (fireWaitTime <= 0)
                {
                    Shoot();
                }
            }
            else
            {
                Reload();
            }
        }
        else
        {
            if (currentAccuracy < data.accuracy * data.accuracyMultiplier)
            {
                currentAccuracy += data.accuracyResetRate * Time.deltaTime;
                if (currentAccuracy > data.accuracy * data.accuracyMultiplier)
                {
                    ResetAccuracy();
                }
            }
        }
        movementMultiplier = Mathf.Pow(data.movementAccuracyMultiplierBase, -Vector3.Distance(transform.position, lastPosition) / Time.deltaTime);
        lastPosition = transform.position;
	}

    public void Fire()
    {
        if (!reloading)
        {
            firing = true;
        }
    }

    public void StopFiring()
    {
        firing = false;
    }

    public float GetCurrentAccuracy()
    {
        return currentAccuracy;
    }

    public int GetBulletsRemaining()
    {
        return bulletsRemaining;
    }

    public void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }

    protected virtual void LaunchProjectile(){}

    private void Shoot()
    {
        LaunchProjectile();
        bulletsRemaining--;
        currentAccuracy *= data.accuracyMultiplierPerShot;
        fireWaitTime = 1 / data.fireRate;
    }

    private IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(data.reloadTime);
        bulletsRemaining = data.magazineSize;
        ResetAccuracy();
        reloading = false;
        yield return null;
    }
    private void ResetAccuracy()
    {
        currentAccuracy = data.accuracy * data.accuracyMultiplier;
    }
}
