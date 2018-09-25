using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour {
    public enum CoverType { Short, Tall};
    public CoverType coverType = CoverType.Tall;
    public List<Transform> corners = new List<Transform>();
}
