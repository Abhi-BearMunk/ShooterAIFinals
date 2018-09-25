using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public string pickupName;
    public float rotateSpeed = 30;

    private bool disable = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!disable)
        {
            if (other.GetComponent<HitBox>())
            {
                other.GetComponent<HitBox>().myPlayer.AddWeaponToInventory(pickupName);
                disable = true;
                Destroy(gameObject);
            }
        }
    }
}
