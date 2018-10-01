using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socrates_AI : MonoBehaviour {
    public Player player;
    public Pickup pickup;
    public Cover cover;
    public Player target;

    public Transform lookTarget;

    public bool playerFound = false;
    public GameObject enemy;

    public Vector3 enemyDistance;

    public bool coverFound = false;
    public bool inCover = false;
    public Vector3 coverPoint;

    public Animator anim;

    public float dist;

    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Found Cover", coverFound);
        anim.SetBool("In Cover", coverFound);
        anim.SetBool("Player Found", playerFound);
        anim.SetFloat("EnemyDistance", Vector3.Distance(transform.position, enemy.transform.position));

        transform.LookAt(lookTarget);

        dist = Vector3.Distance(transform.position, enemy.transform.position);
    }
    private void OnTriggerStay(Collider other)
    {
       
    }
}
