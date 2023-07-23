using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

namespace Towers { 
    internal class SniperTower : Tower
    {
        public float ReadyTime;

        protected override void Shoot()
        {
            AudioManager.Instance.PlaySFX("Sniper");
            base.Shoot();
        }

        protected override void SelectTarget()
        {
            // Shoots until it kills or target leaves range
            if (currentTarget == null)
            {
                currentTarget = MapControllerHelper.FindHighestPriorityInRange(this.transform.position, Range);
                //nextShoot += ReadyTime;
            }                
            else if (MapControllerHelper.GetDistance(this.transform.position, currentTarget.transform.position) > Range)
            {
                currentTarget = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, Range);
        }
    }
}