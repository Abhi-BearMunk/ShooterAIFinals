using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Socrates_Find_Cover : StateMachineBehaviour {

    GameObject socrates;
    //Player pl;
    Socrates_AI data;

    public Transform dest;
    //bool foundCover;

    public float threshold = 0.1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        socrates = animator.gameObject;
        data = socrates.GetComponent<Socrates_AI>();
        //pl = socrates.GetComponent<Player>();

        data.playerFound = false;

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        List<Cover> covers = data.player.GetDetectedOfType<Cover>();
        if (covers.Count > 0)
        {
            data.cover = covers[0];
            data.coverFound = true;
            //return Status.Success;
            //SetPoint();
        }
        else
        {
            data.cover = null;
            //return Status.Failure;
        }

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
    void FindCover()
    {
        List<Cover> covers = data.player.GetDetectedOfType<Cover>();
        if (covers.Count > 0)
        {
            data.cover = covers[0];
            //return Status.Success;
            //SetPoint();
        }
        else
        {
            data.cover = null;
            //return Status.Failure;
        }
    }
    void SetPoint()
    {
        if (data.cover && data.cover.coverType == Cover.CoverType.Short && data.target)
        {
            Ray ray = new Ray(data.target.transform.position, data.cover.transform.position - data.target.transform.position);
            RaycastHit hit;
            if (data.cover.GetComponent<Collider>().Raycast(ray, out hit, Vector3.Distance(data.cover.transform.position, data.target.transform.position)))
            {
                data.coverPoint = 2 * data.cover.transform.position - hit.point;
                data.coverFound = true;
                //return Status.Success;
            }
        }
    }
  

    void MoveToCover()
    {
        if (data.coverFound)
        {
            data.player.Setdestination(data.coverPoint);
            if (Vector3.Distance(data.player.transform.position, data.coverPoint) < threshold)
            {
                data.inCover = true;
                //return Status.Success;
            }
           // return Status.Running;
        }
        //return Status.Failure;
    }
    
}
