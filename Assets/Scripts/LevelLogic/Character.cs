using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    internal abstract class Character : MonoBehaviour
    {
        #region Fields and Functions

        internal int Health { get; private set; }

        internal float MovementSpeed { get; private set; } = 5;

        #endregion

        #region Functions

        protected void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
                StartCoroutine(Die());
        }

        protected IEnumerator Die()
        {
            SpriteRenderer characterSprite = GetComponent<SpriteRenderer>();
            characterSprite.DOFade(0, 0.5f);
            OnDeathEffect();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }


        internal void SlowCharacter(int slowAmount)
        {
            StartCoroutine(RestoreOriginalSpeed(MovementSpeed));
            MovementSpeed -= slowAmount;
        }

        protected IEnumerator RestoreOriginalSpeed(float speed)
        {
            var restoreSpeed = 0.5f;
            var whileCounter = 0;
            yield return new WaitForSeconds(restoreSpeed);
            
            while (MovementSpeed < speed)
            {
                if (whileCounter > 50)
                {
                    Debug.Log("Slow while loop safety quit triggered!");
                    break;
                }

                MovementSpeed += 0.5f;
                yield return new WaitForSeconds(restoreSpeed);
                whileCounter++;
            }
            
            if (MovementSpeed != speed)
                MovementSpeed = speed;
        }

        protected abstract void OnDeathEffect();

        #endregion
    }
}
