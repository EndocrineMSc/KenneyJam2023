using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    internal class ScavengerMob : Character
    {
        private void OnEnable()
        {
            CharacterEvents.OnCharacterDeath += GainCurrency;
        }

        private void OnDisable()
        {
            CharacterEvents.OnCharacterDeath -= GainCurrency;
        }

        protected override void OnDamageEffect()
        {
            //ToDo: stop following? 1s
        }

        protected override void OnDeathEffect()
        {
            //none
        }

        private void GainCurrency()
        {
            //ToDo: whatever Currency +1, where Currency is stored
        }
    }
}
