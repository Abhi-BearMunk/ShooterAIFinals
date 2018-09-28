using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abi
{
    public class BehaviourData : MonoBehaviour
    {

        public Player player;
        public Pickup pickup;
        public Cover cover;
        public Player target;

        public bool coverFound = false;
        public bool inCover = false;
        public Vector3 coverPoint;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
