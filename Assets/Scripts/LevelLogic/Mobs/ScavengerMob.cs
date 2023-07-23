using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Utility;

namespace Characters
{
    internal class ScavengerMob : Character
    {
        [SerializeField] private CurrencyPopup _currencyPrefab;
        private readonly float _range = 1.5f;
        private readonly float _scavengeRange = 10f;

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
            var distance = Mathf.Abs(Vector3.Distance(characterObject.transform.position, transform.position));
            if (distance < _scavengeRange)
            {
                var amountCurrency = 4 - characterObject.GetComponent<Character>().TargetPriority;
                if (characterObject != this.gameObject)
                {
                    PlayerData.AddCurrency(amountCurrency);
                    StartCoroutine(SpawnCoins(amountCurrency));
                }
            }
        }

        private IEnumerator SpawnCoins(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_currencyPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }

        private void Update()
        {
            if (!_isDead)
            {
                var highestPrioCharacter = MapControllerHelper.FindHighestPriorityInRange(transform.position, _range);
                if (highestPrioCharacter != null && highestPrioCharacter != this.gameObject)
                    MovementSpeed = highestPrioCharacter.GetComponent<Character>().MovementSpeed;
                else
                    MovementSpeed = _maxMovementSpeed;
            }
            else
                MovementSpeed = 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, _range);
            Gizmos.DrawCube(transform.position, new(_scavengeRange, _scavengeRange));
        }
    }
}
