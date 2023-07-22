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

        protected virtual void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindFurthestAdvancedCharacterInRange(this.gameObject.transform.position, Range);
        }

        protected virtual void Shoot()
        {
            bool KilledTarget = currentTarget.GetComponent<Character>().TakeDamage(damage);

            // Only relevant for gun-type towers (they only switch target on kill)
            if (KilledTarget)
                currentTarget = null;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            SelectTarget();
            if (currentTarget != null && Time.deltaTime >= attackSpeed)
            {
                Shoot();
            }
        }

        #endregion
    }

}