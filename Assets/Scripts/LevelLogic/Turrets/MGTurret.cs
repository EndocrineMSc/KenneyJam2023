using Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Towers
{
    internal class MGTurret : Tower
    {
        public float ReadyTime;

        protected override void Shoot()
        {
            AudioManager.Instance.PlaySFX("MG");
            base.Shoot();
        }

        protected override void SelectTarget()
        {
            // Shoots until it kills or target leaves range
            if (currentTarget == null) { 
                base.SelectTarget();
                nextShoot = Time.time + attackSpeed + ReadyTime; // CA-CHUNK gun ready sound?
            }       
            else if (MapControllerHelper.GetDistance(this.transform.position, currentTarget.transform.position) > Range)
            {
                currentTarget = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position, Range);
        }
    }
}
