using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socrates_Chase : StateMachineBehaviour
{

    GameObject socrates;

    Socrates_AI data;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        socrates = animator.gameObject;
        data = socrates.GetComponent<Socrates_AI>();


    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        data.player.transform.LookAt(data.enemy.transform.position);
        //data.player.RotateWeapon(data.enemy.transform.position, 2);
        data.player.Setdestination(data.enemy.transform.position);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
