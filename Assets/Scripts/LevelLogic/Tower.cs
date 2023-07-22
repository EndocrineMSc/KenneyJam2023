using Characters;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

namespace Towers
{

    internal abstract class Tower : MonoBehaviour
    {
        #region Fields and Functions
        [SerializeField]
        public int Range ;

        [SerializeField]
        public int damage;
        
        [SerializeField]
        public float attackSpeed;
        [SerializeField]
        public float TravelSpeed;
        
        internal GameObject currentTarget;
        
        [SerializeField]
        public int TargetPriority;
        internal Turret TurretName;

        protected Projectile projectile;

        #endregion

        #region Functions

        protected virtual void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindFurthestAdvancedCharacterInRange(this.gameObject.transform.position, Range);
        }

        protected virtual void Shoot()
        {
            bool KilledTarget = currentTarget.GetComponent<Characters.Character>().TakeDamage(damage);

            // Only relevant for gun-type towers (they only switch target on kill)
            if (KilledTarget)
                currentTarget = null;
        }

        // Start is called before the first frame update
        void Awake()
        {
            projectile = this.GetComponent<Projectile>();
        }

        // Update is called once per frame
        void Update()
        {
            SelectTarget();
            if (currentTarget != null && Time.deltaTime >= attackSpeed)
            {
                AnimateProjectile();
                Shoot();
            }
        }

        protected void AnimateProjectile()
        {
            if (projectile == null)
                return;

            var direction = this.transform.position - currentTarget.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Projectile newProjectile = Instantiate(projectile, this.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            newProjectile.DefineTarget(currentTarget.transform.position, TravelSpeed);
        }

        #endregion
    }

}