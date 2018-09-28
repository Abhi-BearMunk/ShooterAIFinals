using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

namespace Abi
{
    // Condition Parent
    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class AbiConditionNode : ConditionNode
    {
        protected BehaviourData data;
        // Use this for initialization
        public override void Start()
        {
            data = (BehaviourData)blackboard.GetObjectVar("data");
        }

        // Update is called once per frame
        public override Status Update()
        {
            return Status.Success;
        }
    }

    // Conditions
    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class CanSeePickup : AbiConditionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            List<Pickup> pickups = data.player.GetDetectedOfType<Pickup>();
            if (pickups.Count > 0)
            {
                data.pickup = pickups[0];
                return Status.Success;
            }
            else
            {
                data.pickup = null;
                return Status.Failure;
            }
        }
    }

    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class CanSeeCover : AbiConditionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            List<Cover> covers = data.player.GetDetectedOfType<Cover>();
            if (covers.Count > 0)
            {
                data.cover = covers[0];
                return Status.Success;
            }
            else
            {
                data.cover = null;
                return Status.Failure;
            }
        }
    }

    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class ShortCoverFound : AbiConditionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            if(data.coverFound)
            {
                return Status.Success;          
            }
            return Status.Failure;
        }
    }

    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class IsInCover : AbiConditionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            if (data.inCover)
            {
                return Status.Success;
            }
            return Status.Failure;
        }
    }

    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class CanSeeEnemy : AbiConditionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            List<Player> enemies = data.player.GetDetectedOfType<Player>();
            if (enemies.Count > 0)
            {
                data.target = enemies[0];
                return Status.Success;
            }
            else
            {
                data.target = null;
                return Status.Failure;
            }
        }
    }

    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class IsCrouching : AbiConditionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            if (data.player.IsCrouching())
            {
                return Status.Success;
            }
            return Status.Failure;
        }
    }

    [NodeInfo(category = "Abi/Conditions/", icon = "")]
    public class isReloading : AbiConditionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            if(data.player.IsReloading())
            {
                return Status.Success;
            }
            return Status.Failure;
        }
    }


    // Action Parent
    [NodeInfo(category = "Abi/Actions/", icon = "")]
    public class AbiActionNode : ActionNode
    {
        protected BehaviourData data;
        // Use this for initialization
        public override void Start()
        {
            data = (BehaviourData)blackboard.GetObjectVar("data");
        }

        // Update is called once per frame
        public override Status Update()
        {
            return Status.Success;
        }
    }

    // Actions
    [NodeInfo(category = "Abi/Actions/", icon = "")]
    public class GoToPickup : AbiActionNode
    {
        public float threshold = 0.1f;
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            if(data.pickup)
            {
                data.player.Setdestination(data.pickup.transform.position);
                if(Vector3.Distance(data.player.transform.position, data.pickup.transform.position) < threshold)
                {
                    return Status.Success;
                }
                return Status.Running;
            }
            return Status.Failure;
        }
    }

    // Actions
    [NodeInfo(category = "Abi/Actions/", icon = "")]
    public class SetShortCoverPoint : AbiActionNode
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override Status Update()
        {
            if (data.cover && data.cover.coverType == Cover.CoverType.Short && data.target)
            {
                Ray ray = new Ray(data.target.transform.position, data.cover.transform.position - data.target.transform.position);
                RaycastHit hit;
                if (data.cover.GetComponent<Collider>().Raycast(ray, out hit, Vector3.Distance(data.cover.transform.position, data.target.transform.position)))
                {
                    data.coverPoint = 2 * data.cover.transform.position - hit.point;
                    data.coverFound = true;
                    return Status.Success;
                }
            }
            return Status.Failure;
        }

        [NodeInfo(category = "Abi/Actions/", icon = "")]
        public class GoToCover : AbiActionNode
        {
            public float threshold = 0.1f;
            // Use this for initialization
            public override void Start()
            {
                base.Start();
            }

            // Update is called once per frame
            public override Status Update()
            {
                if (data.coverFound)
                {
                    data.player.Setdestination(data.coverPoint);
                    if (Vector3.Distance(data.player.transform.position, data.coverPoint) < threshold)
                    {
                        data.inCover = true;
                        return Status.Success;
                    }
                    return Status.Running;
                }
                return Status.Failure;
            }
        }

        [NodeInfo(category = "Abi/Actions/", icon = "")]
        public class LookAtTarget : AbiActionNode
        {
            public float threshold = 0.1f;
            // Use this for initialization
            public override void Start()
            {
                base.Start();
            }

            // Update is called once per frame
            public override Status Update()
            {
                if (data.target)
                {
                    data.player.RotateWeapon(data.target.bodyParts["Head"].position, threshold);
                    return Status.Running;
                }
                return Status.Failure;
            }
        }

        [NodeInfo(category = "Abi/Actions/", icon = "")]
        public class ToggleCrouch : AbiActionNode
        {
            public bool crouch;
            // Use this for initialization
            public override void Start()
            {
                base.Start();
            }

            // Update is called once per frame
            public override Status Update()
            {
                data.player.Crouch(crouch);
                return Status.Success;
            }
        }

        [NodeInfo(category = "Abi/Actions/", icon = "")]
        public class Shoot : AbiActionNode
        {
            // Use this for initialization
            public override void Start()
            {
                base.Start();
            }

            // Update is called once per frame
            public override Status Update()
            {
                data.player.Shoot();
                return Status.Running;
            }
        }

        [NodeInfo(category = "Abi/Actions/", icon = "")]
        public class StopShooting : AbiActionNode
        {
            // Use this for initialization
            public override void Start()
            {
                base.Start();
            }

            // Update is called once per frame
            public override Status Update()
            {
                data.player.StopShooting();
                return Status.Success;
            }
        }

        [NodeInfo(category = "Abi/Actions/", icon = "")]
        public class Wander : AbiActionNode
        {
            public float wanderTime = 10;
            public float threshold = 1f;
            float timer = 0;

            Vector3 wanderPosition;
            // Use this for initialization
            public override void Start()
            {
                base.Start();
                SetWanderPosition();
            }

            // Update is called once per frame
            public override Status Update()
            {
                timer += Time.deltaTime;
                if(timer >= wanderTime || Vector3.Distance(data.player.transform.position, wanderPosition) < threshold)
                {
                    SetWanderPosition();
                    timer = 0;
                }
                return Status.Running;
            }

            void SetWanderPosition()
            {
                wanderPosition = new Vector3(Random.Range(-21.5f, 21.5f), 0, Random.Range(-33f, 33f));
                data.player.Setdestination(wanderPosition);
            }
        }
    }
}
