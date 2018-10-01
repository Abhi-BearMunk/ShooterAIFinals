using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socrates_Go_To_Cover : StateMachineBehaviour
{

    GameObject socrates;
    //Player pl;
    Socrates_AI data;

    Transform coverChild;

    //public Transform dest;
    //bool foundCover;

    //public float threshold = 0.1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        socrates = animator.gameObject;
        data = socrates.GetComponent<Socrates_AI>();


        
        coverChild = data.cover.GetComponentInChildren<Transform>();

        data.coverPoint = coverChild.transform.position + new Vector3(-1,0,4) ;

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data.player.Setdestination(data.coverPoint);
        if (data.gameObject.transform.position.x == data.coverPoint.x)
        {
            data.inCover = true;
        }
        data.lookTarget = coverChild;
        //data.player.transform.LookAt(data.cover.transform);
        //data.player.RotateWeapon(data.enemy.transform.position, 2);

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data.coverPoint = data.transform.position ;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
