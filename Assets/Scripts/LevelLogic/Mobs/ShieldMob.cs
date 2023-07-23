using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    internal class ShieldMob : Character
    {
        public int MaxShield;
        public int CurrentShield { get; private set; }

        [SerializeField] private float _shieldGenerationTimer = 1;
        private float _currentTime = 0;

        protected override void SubtractDamage(int damage)
        {
            if (CurrentShield > 0) {
                CurrentShield -= damage;
                if (CurrentShield < 0)
                    CurrentShield = 0;
            }              
            else
                Health -= damage;

        }

        protected override void InitializeData()
        {
            base.InitializeData();
            CurrentShield = MaxShield;
        }

        protected override void OnDamageEffect()
        {
            //none
        }

        protected override void OnDeathEffect()
        {
            //none
        }

        private void Update()
        {
            if (_currentTime < _shieldGenerationTimer && CurrentShield < MaxShield) 
            {
                _currentTime += Time.deltaTime;
            }
            else if (_currentTime >= _shieldGenerationTimer && CurrentShield < MaxShield) 
            {
                _currentTime = 0;
                CurrentShield++;
            }
            else
            {
                _currentTime = 0;
            }
        }
    }
}
