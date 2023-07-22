using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    //This mob will be instantiated multiple times when purchased for the Spawner
    internal class SwarmMob : Character
    {
        protected override void OnDamageEffect()
        {
            //no on damage effect
        }

        protected override void OnDeathEffect()
        {
            //no on death effect
        }
    }
}
