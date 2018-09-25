using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDebug : MonoBehaviour {

    public bool shoot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(shoot)
        {
            GetComponent<Weapon>().Fire();
        }
        else
        {
            GetComponent<Weapon>().StopFiring();
        }
	}
}
