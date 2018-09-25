using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSphere : MonoBehaviour {
    public Color debugColor;
    public float radius;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
