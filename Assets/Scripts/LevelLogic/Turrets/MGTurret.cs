using Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Towers
{

    internal class MGTurret : Tower
    {
        protected override void SelectTarget()
        {
            // Shoots until it kills or target leaves range
            if (currentTarget == null)
                base.SelectTarget();
            else if (MapControllerHelper.GetDistance(this.transform.position, currentTarget.transform.position) > Range)
            {
                currentTarget = null;
            }
        }
    }
}
