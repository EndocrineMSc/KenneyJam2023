using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    internal class TankMob : Character
    {
        protected override void OnDamageEffect()
        {
            SlowCharacter(MovementSpeed);
        }

        protected override void OnDeathEffect()
        {
            //no on death effect
        }
    }
}
