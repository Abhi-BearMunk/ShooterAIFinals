using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerData data;
    public List<Weapon> playerWeapons;
    public List<Detector> playerDetectors;
    public Detectable highLevelCollider;
    public Dictionary<string, Transform> bodyParts = new Dictionary<string, Transform>();

    private Dictionary<string, Weapon> weaponKeys = new Dictionary<string, Weapon>();
    private Dictionary<string, Weapon> inventory = new Dictionary<string, Weapon>();
    private Weapon currentWeapon;

    private NavMeshAgent navAgent;
    private float speed = 10;
    private float angularSpeed = 120;

    private bool crouched = false;
    public float standingAccuracyMultiplier = 0.8f;
    public float crouchHeight = 0.5f;
    public float crouchSpeedMultiplier = 0.5f;

    private void Awake()
    {
        //Hack!!!
        GetComponent<Rigidbody>().drag = 10;

        foreach (Detector d in gameObject.GetComponentsInChildren<Detector>())
        {
            if(!playerDetectors.Contains(d))
            {
                playerDetectors.Add(d);
            }
        }
        foreach (Detector d in playerDetectors)
        {
            d.parentObject = this.gameObject;
        }
    }
    // Use this for initialization
    void Start () {
        foreach(HitBox limb in gameObject.GetComponentsInChildren<HitBox>())
        {
            bodyParts.Add(limb.name, limb.transform);
        }
        foreach(Weapon w in playerWeapons)
        {
            WeaponData wData = w.data;
            wData.owner = this;
            wData.myPlayerLayer = this.gameObject.layer;
            w.data = wData;
            weaponKeys.Add(w.name, w);
        }
        navAgent = GetComponent<NavMeshAgent>();
        //HACK: Remove later
        AddWeaponToInventory("ShotGun");
	}
	
	// Update is called once per frame
	void Update () {
        navAgent.speed = speed * (crouched ? crouchSpeedMultiplier : 1);
	}

    public void TakeDamage(float damage)
    {
        data.health = Mathf.Max(0, data.health - damage);
        if(data.health <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }

    public void ChangeWeapon(string weaponName)
    {
        if (inventory.ContainsKey(weaponName))
        {
            StartCoroutine("ActivateWeapon", weaponName);
        }
    }

    // AI
    public void Shoot()
    {
        if(currentWeapon)
        {
            currentWeapon.Fire();
        }
    }

    // AI
    public void StopShooting()
    {
        if (currentWeapon)
        {
            currentWeapon.StopFiring();
        }
    }

    // AI
    public void Reload()
    {
        if (currentWeapon)
        {
            currentWeapon.Reload();
        }
    }

    // AI
    public bool IsReloading()
    {
        return currentWeapon.IsReloading();
    }

    public void AddWeaponToInventory(string weaponName)
    {
        if(!inventory.ContainsKey(weaponName))
        {
            inventory.Add(weaponName, weaponKeys[weaponName]);
            ChangeWeapon(weaponName);
        }
    }

    // AI
    public void SetMovementSpeed(float multiplier = 1)
    {
        multiplier = Mathf.Clamp(multiplier, 0, 1);
        speed = multiplier * data.maxSpeed;
        navAgent.speed = speed;
    }

    // AI
    public void Setdestination(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }

    // AI
    public List<T> GetDetectedOfType<T>() where T : MonoBehaviour
    {
        List<T> ret = new List<T>();
        foreach (Detector d in playerDetectors)
        {
            ret.AddRange(d.GetDetectedOfType<T>());
        }
        ret.Sort((r1, r2) => Vector3.Distance(r1.transform.position, transform.position).CompareTo(Vector3.Distance(r2.transform.position, transform.position)));
        return ret;
    }

    // AI
    public bool RotateWeapon(Vector3 lookAtPoint, float threshold, float multiplier = 1)
    {
        if (currentWeapon)
        {
            multiplier = Mathf.Clamp(multiplier, 0, 1);
            angularSpeed = multiplier * data.maxAngularSpeed;

            Vector3 lookDirection = (lookAtPoint - currentWeapon.gameObject.transform.position).normalized;

            Vector3 lookDirectionXZ = new Vector3(lookDirection.x, 0, lookDirection.z);
            Vector3 forwardXZ = new Vector3(currentWeapon.gameObject.transform.forward.x, 0, currentWeapon.gameObject.transform.forward.z);

            bool verticalSet = Mathf.Abs(lookDirection.y - currentWeapon.gameObject.transform.forward.y) < threshold;
            bool horizontalSet = Vector3.Angle(lookDirectionXZ, forwardXZ) < threshold * Mathf.Rad2Deg;

            float down = 1;
            float right = 1;
            if (lookDirection.y > currentWeapon.gameObject.transform.forward.y)
            {
                down = -1;
            }
            if (Vector3.Cross(forwardXZ, lookDirectionXZ).y < 0)
            {
                right = -1;
            }

            //Debug.DrawRay(currentWeapon.gameObject.transform.position, forwardXZ, Color.yellow);
            //Debug.DrawRay(currentWeapon.gameObject.transform.position, lookDirectionXZ, right > 0 ? Color.green : Color.red);

            if (!horizontalSet)
            {
                transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime * right);
            }
            if (!verticalSet)
            {
                currentWeapon.gameObject.transform.Rotate(Vector3.right, angularSpeed * Time.deltaTime * down);
            }

            return horizontalSet && verticalSet;
        }
        Debug.LogWarning("Player does not have any weapon equipped to rotate");
        return false;
    }

    // AI
    public bool CanDetect(Player p)
    {
        foreach(Detector detector in playerDetectors)
        {
            if(detector.IsDetectable(p.highLevelCollider))
            {
                return true;
            }
        }
        return false;
    }

    // AI
    public void Crouch(bool toggle)
    {
        if(toggle && !crouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
            crouched = true;
        }
        else if (!toggle && crouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            crouched = false;
        }
    }

    // AI 
    public bool IsCrouching()
    {
        return crouched;
    }

    public float GetAccuracyMultiplier()
    {
        return crouched ? 1 : standingAccuracyMultiplier;
    }

    IEnumerator ActivateWeapon(string weaponName)
    {
        if(currentWeapon)
        {
            currentWeapon.StopFiring();
            currentWeapon.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(data.WeaponChangeTime);
        currentWeapon = inventory[weaponName];
        currentWeapon.gameObject.SetActive(true);
        yield return null;
    }
}
