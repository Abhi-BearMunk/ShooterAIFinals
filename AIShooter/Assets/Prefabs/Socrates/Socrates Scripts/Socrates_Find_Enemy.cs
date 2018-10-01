using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socrates_Find_Enemy : StateMachineBehaviour
{

    GameObject socrates;
    //Player pl;
    Socrates_AI data;

    //Transform coverChild;

    //public Transform dest;
    //bool foundCover;

    //public float threshold = 0.1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        socrates = animator.gameObject;
        data = socrates.GetComponent<Socrates_AI>();

        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        List<Player> players = data.player.GetDetectedOfType<Player>();
        if (players.Count > 0)
        {
            data.player = players[0];
            data.playerFound = true;
            //return Status.Success;
            //SetPoint();
        }
        else
        {
            data.cover = null;
            //return Status.Failure;
        }

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data.enemy = data.player.gameObject;
        data.coverFound = false;
        data.inCover = false;
    }
}
