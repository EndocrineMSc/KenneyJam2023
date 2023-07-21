using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    internal abstract class Character : MonoBehaviour
    {
        #region Fields and Functions

        public int Health;
        public int MaxHealth;
        public float MovementSpeed;
        public float MaxMovementSpeed;
        public int TargetPriority;

        #endregion

        #region Functions

        protected virtual void Awake()
        {
            Health = MaxHealth;
            MovementSpeed = MaxMovementSpeed;
        }

        protected bool TakeDamage(int damage)
        {
            Health -= damage;

            bool stillAlive = Health <= 0;

            if (!stillAlive) {
                StartCoroutine(Die());
                TargetPriority = -1;
            }

            OnDamageEffect();

            // Towers may decide to change target only when the target died
            return stillAlive;
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
            // Do this multiplicative? Reduce by % of max speed
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

        protected abstract void OnDamageEffect();

        #endregion
    }
}
