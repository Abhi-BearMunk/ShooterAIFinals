using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : Detector {

    public LayerMask ignoreLayers;
    public float visionAngle;
    public override bool IsDetectable(Detectable target)
    {
        if (base.IsDetectable(target))
        {
            RaycastHit hit;
            foreach (Transform t in target.extremes)
            {
                if (Vector3.Angle(t.position - transform.position, transform.forward) < visionAngle && Physics.Raycast(transform.position, t.position - transform.position, out hit, Mathf.Infinity, ~ignoreLayers))
                {
                    if (hit.collider.GetComponent<Detectable>() && hit.collider.GetComponent<Detectable>() == target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        return false;
    }
}
