using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour {
    public List<Transform> points = new List<Transform>();
    public Vector4[] positions;
    public float[] radiuses;
    public float[] intensities;

    public Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;

        material.SetInt("_Points_Length", positions.Length);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector4(points[i].position.x, points[i].position.y, points[i].position.z, Random.Range(0f, 0.8f));
            //radiuses[i] = Random.Range(0f, 0.2f);
            //intensities[i] = Random.Range(0f, 1f);
            //material.SetVector("_Points" + i.ToString(), positions[i]);

            //Vector2 properties = new Vector2(radiuses[i], intensities[i]);
            //material.SetVector("_Properties" + i.ToString(), properties);
        }
        material.SetVectorArray("_Points", positions);
    }

    private void Update()
    {
        material = GetComponent<Renderer>().material;

        material.SetInt("_Points_Length", positions.Length);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector4(points[i].position.x, points[i].position.y, points[i].position.z, positions[i].w);
            //radiuses[i] = Random.Range(0f, 0.2f);
            //intensities[i] = Random.Range(0f, 1f);
            //material.SetVector("_Points" + i.ToString(), positions[i]);

            //Vector2 properties = new Vector2(radiuses[i], intensities[i]);
            //material.SetVector("_Properties" + i.ToString(), properties);
        }
        material.SetVectorArray("_Points", positions);
    }
}

//material = GetComponent<Renderer>().material;

//        material.SetInt("_Points_Length", positions.Length);
//        for (int i = 0; i<positions.Length; i++)
//        {
//            positions[i] = new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 0.5f), 1f);
//        }
//        material.SetVectorArray("_Points", positions);