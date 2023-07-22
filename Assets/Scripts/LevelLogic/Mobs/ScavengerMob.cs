using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Characters
{
    internal class ScavengerMob : Character
    {
        [SerializeField] private CurrencyPopup _currencyPrefab;

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

        private void GainCurrency(GameObject characterObject)
        {
            var amountCurrency = characterObject.GetComponent<Character>().TargetPriority;
            PlayerData.AddCurrency(amountCurrency);
            Instantiate(_currencyPrefab, transform.position, Quaternion.identity);
        }
    }
}
