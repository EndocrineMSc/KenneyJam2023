using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{

    internal abstract class Tower : MonoBehaviour
    {

        #region Fields and Functions
        [SerializeField]
        public float Range ;

        [SerializeField]
        internal int damage;
        
        [SerializeField]
        internal float attackSpeed;
        [SerializeField]
        internal float travelTime; //Maybe, maybe not? Projectiles could always just go straight, to make it easy
        
        internal Character currentTarget;
        
        [SerializeField]
        public int TargetPriority;

        #endregion

        #region Functions

        internal void Shoot()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Distance and 
            // if(currentTarget = null || Distance(this, currentTarget) > Range)
            {
                // currentTarget = GetNearestCharacter(this, Range);
                
            }

            // I think there is some clever way to check this
            double lastUpdate = 0; //dummy for how I would do it
            double currentTime = 1;
            if(currentTime- lastUpdate > attackSpeed)
            {
            }
        }

        #endregion
    }

}