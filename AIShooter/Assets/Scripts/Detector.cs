using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    public List<Detectable> inRange = new List<Detectable>();
    public Color debugColor = new Color();
    public GameObject parentObject;
 	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug
        List<Player> pList = GetDetectedOfType<Player>();
        foreach (Player p in pList)
        {
            Debug.DrawLine(transform.position, p.transform.position, Color.red);
        }
        List<Pickup> pUList = GetDetectedOfType<Pickup>();
        foreach (Pickup p in pUList)
        {
            Debug.DrawLine(transform.position, p.transform.position, Color.blue);
        }
        List<Cover> CList = GetDetectedOfType<Cover>();
        foreach (Cover p in CList)
        {
            Debug.DrawLine(transform.position, p.transform.position, Color.green);
        }
    }

    public virtual bool IsDetectable(Detectable target)
    {
        return inRange.Contains(target);
    }

    public List<T> GetDetectedOfType<T>() where T: MonoBehaviour
    {
        List<T> ret = new List<T>();
        List<Detectable> toBeRemoved = new List<Detectable>();
        foreach(Detectable d in inRange)
        {
            if(d)
            {
                if (d.parentGameObject.GetComponent<T>() && IsDetectable(d))
                {
                    ret.Add(d.parentGameObject.GetComponent<T>());
                }
            }
            else
            {
                toBeRemoved.Add(d);
            }
        }
        foreach (Detectable destroyed in toBeRemoved)
        {
            inRange.Remove(destroyed);
        }
        ret.Sort((r1, r2) => Vector3.Distance(r1.transform.position, transform.position).CompareTo(Vector3.Distance(r2.transform.position, transform.position)));
        return ret;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Detectable>() && parentObject.layer != other.GetComponent<Detectable>().parentGameObject.layer)
        {
            inRange.Add(other.GetComponent<Detectable>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Detectable>())
        {
            inRange.Remove(other.GetComponent<Detectable>());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        List<Player> pList = GetDetectedOfType<Player>();
        foreach(Player p in pList)
        {
            Gizmos.DrawSphere(p.transform.position, 0.4f);
        }
    }
}
