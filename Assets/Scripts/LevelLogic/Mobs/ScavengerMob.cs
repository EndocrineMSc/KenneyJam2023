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
            var amountCurrency = 4 - characterObject.GetComponent<Character>().TargetPriority;
            PlayerData.AddCurrency(amountCurrency);
            StartCoroutine(SpawnCoins(amountCurrency));
        }

        private IEnumerator SpawnCoins(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_currencyPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
