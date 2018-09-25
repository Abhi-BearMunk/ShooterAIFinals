using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNav : MonoBehaviour {

    public Transform dest;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<NavMeshAgent>().SetDestination(dest.position);
	}
}
