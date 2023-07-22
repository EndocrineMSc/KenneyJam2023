using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

namespace Towers { 
    internal class SniperTower : Tower
    {
        protected override void SelectTarget()
        {
            // Shoots until it kills or target leaves range
            if (currentTarget == null)
                currentTarget = MapControllerHelper.FindHighestPriorityInRange(this.transform.position, Range);
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