using Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Towers
{
    internal class BombTurret : Tower
    {
        [SerializeField]
        public float AreaOfEffect;

        protected ParticleSystem explosion;

        protected override void Awake()
        {
            base.Awake();
            explosion = GetComponentInChildren<ParticleSystem>();
            projectile = null;
        }

        protected override void Shoot()
        {
            MapControllerHelper.FindCharactersInRange(currentTarget.transform.position, AreaOfEffect).
                ForEach(character => character.GetComponent<Character>().TakeDamage(damage));

            // No need to set current target to null, as this tower always retargets
        }

        protected override void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindHighestPriorityInRange(this.transform.position, Range);
        }

        protected override void AnimateProjectile()
        {
            base.AnimateProjectile();

            Instantiate(explosion, currentTarget.transform.position, currentTarget.transform.rotation);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, Range);
        }
    }

}