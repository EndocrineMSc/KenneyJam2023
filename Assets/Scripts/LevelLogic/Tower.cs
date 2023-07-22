using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{

    internal abstract class Tower : MonoBehaviour
    {
        #region Fields and Functions
        [SerializeField]
        public int Range ;

        [SerializeField]
        internal int damage;
        
        [SerializeField]
        internal float attackSpeed;
        [SerializeField]
        internal float travelTime; //Maybe, maybe not? Projectiles could always just go straight, to make it easy
        
        internal GameObject currentTarget;
        
        [SerializeField]
        public int TargetPriority;
        internal Turret TurretName;

        #endregion

        #region Functions

        internal void Shoot()
        {
            if(currentTarget == null) {
                currentTarget = MapControllerHelper.FindClosestCharacterInRange(this.gameObject.transform.position, Range);
            } else
            {

            }
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