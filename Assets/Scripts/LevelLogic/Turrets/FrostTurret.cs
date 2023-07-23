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
            AudioManager.Instance.PlaySFX("Frost");
            base.Shoot();

            if(currentTarget != null)
                currentTarget.GetComponent<Character>().SlowCharacter(SlowAmount);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position, Range);
        }
    }

}
