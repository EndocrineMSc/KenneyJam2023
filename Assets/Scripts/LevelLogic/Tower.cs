using Characters;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace Towers
{

    internal abstract class Tower : MonoBehaviour
    {
        #region Fields and Functions
        [SerializeField]
        public float Range ;

        [SerializeField]
        public int damage;
        
        [SerializeField]
        public float attackSpeed;
        [SerializeField]
        public float TravelSpeed;
        
        internal GameObject currentTarget;
                
        public Turret TurretName;

        protected Projectile projectile;

        protected float nextShoot;

        [SerializeField,TextArea]
        protected string tooltipDescription;

        #endregion

        #region Functions

        protected virtual void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindFurthestAdvancedCharacterInRange(this.gameObject.transform.position, Range);

            if (currentTarget.GetComponent<Characters.Character>() == null)
                currentTarget = null;
        }

        protected virtual void Shoot()
        {
            bool KilledTarget = !currentTarget.GetComponent<Characters.Character>().TakeDamage(damage);

            // Only relevant for gun-type towers (they only switch target on kill)
            if (KilledTarget)
                currentTarget = null;
        }

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            projectile = GetComponentInChildren<Projectile>();
            if (projectile != null)
                projectile.GetComponent<SpriteRenderer>().enabled = false;

            nextShoot = Time.time;
            tooltipDescription = TurretName.ToString() + "\n \n" + tooltipDescription + "\n \n" + "Attack Speed: " + attackSpeed + "\n" + "Damage: " + damage;
        }

        // Update is called once per frame
        void Update()
        {
            SelectTarget();
            if (currentTarget != null && Time.time >= nextShoot)
            {
                AnimateProjectile();
                // Wait for? MapControllerHelper.GetDistance(this.transform.position, currentTarget.transform.position) * TravelSpeed
                Shoot();
                nextShoot = Time.time + attackSpeed;
            }
        }

        protected virtual void AnimateProjectile()
        {
            if (projectile == null)
                return;

            var direction = this.transform.position - currentTarget.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Projectile newProjectile = Instantiate(projectile, this.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            newProjectile.DefineTarget(currentTarget.transform.position, TravelSpeed);
        }

        protected void OnMouseEnter()
        {
            Tooltip.Instance.ShowTooltip(tooltipDescription);
        }

        protected void OnMouseExit()
        {
            Tooltip.Instance.HideTooltip();
        }

        #endregion
    }

}