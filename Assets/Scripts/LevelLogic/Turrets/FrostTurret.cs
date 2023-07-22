using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    internal class FrostTurret : Tower
    {
        [SerializeField]
        public float SlowAmount;

        protected override void Shoot()
        {
            base.Shoot();
            currentTarget.GetComponent<Character>().SlowCharacter(SlowAmount);
        }
    }

}
