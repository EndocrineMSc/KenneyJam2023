using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Characters
{
    internal class HealerMob : Character
    {
        private float _cooldownCounter = 0;
        private readonly float _cooldown = 2;
        [SerializeField] private readonly int _range = 10;
        private readonly int _healAmount = 1;

        protected ParticleSystem HealingEffect;

        protected override void Awake()
        {
            base.Awake();
            _cooldownCounter = _cooldown;
            HealingEffect = GetComponentInChildren<ParticleSystem>();
        }

        protected override void OnDamageEffect()
        {
            //none
        }

        protected override void OnDeathEffect()
        {
            //no on death effect
        }

        private void Update()
        {
            _cooldownCounter += Time.deltaTime;

            if (_cooldownCounter >= _cooldown) 
            {
                _cooldownCounter = 0;
                var closeCharacters = MapControllerHelper.FindCharactersInRange(transform.position, _range);
                Instantiate(HealingEffect, this.transform.position, this.transform.rotation);

                foreach (var character in closeCharacters)
                {
                    character.GetComponent<Character>().Heal(_healAmount);
                }
            }
        }
    }
}
