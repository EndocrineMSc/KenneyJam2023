using Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Towers
{
    internal class BombTurret : Tower
    {
        [SerializeField]
        public int AreaOfEffect;

        protected override void Shoot()
        {
            MapControllerHelper.FindCharactersInRange(currentTarget.transform.position, AreaOfEffect).
                ForEach(character => character.GetComponent<Character>().TakeDamage(damage));

            // No need to set current target to null, as this tower always retargets
        }

        protected override void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindHighestPriorityInRange(this.transform.position, Range);
        }
    }

}