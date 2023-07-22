using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    internal class StackedMob : Character
    {
        [SerializeField] private SwarmMob _swarmy;

        protected override void OnDamageEffect()
        {
            //no on damage effect
        }

        protected override void OnDeathEffect()
        {
            var waypointIndex = GetComponent<CharacterMovement>().TargetWaypointIndex;
            var mob = Instantiate(_swarmy, transform.position, Quaternion.identity).gameObject;
            mob.GetComponent<CharacterMovement>().SetWaypointIndex(waypointIndex);
            CharacterSpawner.Instance.ActiveCharacters.Add(mob);
        }
    }
}
