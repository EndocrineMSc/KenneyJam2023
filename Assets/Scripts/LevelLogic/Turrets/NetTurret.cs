using Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Towers
{

    internal class NetTurret : Tower
    {
        [SerializeField]
        public float AreaOfEffect;

        protected override void SelectTarget()
        {
            currentTarget = MapControllerHelper.FindClosestCharacterInRange(this.gameObject.transform.position, Range);
        }

        protected override void Shoot()
        {
            List<GameObject> charactersInAoE = MapControllerHelper.FindCharactersInRange(currentTarget.transform.position, Range);

            float lowestSpeed = float.MaxValue;

            foreach(GameObject character in charactersInAoE)
            {
                if(character.GetComponent<Character>().MovementSpeed < lowestSpeed) { 
                    lowestSpeed = character.GetComponent<Character>().MovementSpeed;
                }
            }

            // TODO: Should SET the speed to the slowest
            charactersInAoE.ForEach(character => character.GetComponent<Character>().SlowCharacter(lowestSpeed/10));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, Range);
        }
    }
}
