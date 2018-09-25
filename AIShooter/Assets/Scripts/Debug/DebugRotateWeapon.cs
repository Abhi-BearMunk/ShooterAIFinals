using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRotateWeapon : MonoBehaviour {

    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(target)
        {
            GetComponent<Player>().RotateWeapon(target.position, 0.01f, 1);
        }
    }
}
