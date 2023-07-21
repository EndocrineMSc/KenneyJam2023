using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Characters
{
    internal class LanternMob : Character
    {
        private GameObject _lanternLightObject;

        protected override void Awake()
        {
            base.Awake();
            _lanternLightObject = transform.GetChild(0).gameObject;
        }

        protected override void OnDamageEffect()
        {
            //No on damage effect
        }

        protected override void OnDeathEffect()
        {
            DropLantern();
        }

        private void DropLantern()
        {
            Instantiate(_lanternLightObject, transform.position, Quaternion.identity);
        }
    }
}
