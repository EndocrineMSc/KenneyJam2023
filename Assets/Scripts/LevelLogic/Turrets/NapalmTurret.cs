using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers {
    internal class NapalmTurret : Tower
    {
        [SerializeField]
        public float SlowAmount;

        protected override void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindClosestCharacterInRange(this.gameObject.transform.position, Range);
        }
        protected override void Shoot()
        {
            base.Shoot();
            // TODO: Burning?? Flag for being "napalmed"?
            currentTarget.GetComponent<Character>().SlowCharacter(SlowAmount);
        }
    }
}
