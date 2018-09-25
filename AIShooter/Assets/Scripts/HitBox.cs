using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox: MonoBehaviour
{
    public float damageMultiplier;
    public Player myPlayer;
    public float hitVisualTime = 0.1f;

    private bool hit;
    public float hitTimer;

    public void Hit(float damage)
    {
        myPlayer.TakeDamage(damage * damageMultiplier);
        hit = true;
        hitTimer = hitVisualTime;
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    private void Start()
    {
        Material newMaterial = new Material(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = newMaterial;
    }

    void Update()
    {
        if(hit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                hit = false;
            }
        }
    }
}
