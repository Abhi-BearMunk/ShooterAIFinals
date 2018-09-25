using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

namespace Test
{
    [NodeInfo(category = "Test/", icon = "")]
    public class NewBehaviors : ActionNode
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        public override Status Update()
        {
            return Status.Success;
        }
    }

    [NodeInfo(category = "Test/", icon = "")]
    public class MyCondition : ConditionNode
    {
        // Update is called once per frame
        public override Status Update()
        {
            return Status.Success;
        }
    }
}


    
