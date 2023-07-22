using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers {
    internal class ArrowTurret : Tower
    {
        protected override void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindClosestCharacterInRange(this.gameObject.transform.position, Range);
        }
    }

}
