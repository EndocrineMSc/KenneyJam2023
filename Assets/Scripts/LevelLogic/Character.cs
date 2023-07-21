using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class Character : MonoBehaviour
    {
        #region Fields and Functions

        internal float Health { get; private set; }
        [SerializeField]
        public int MaxHealth;

        internal float MovementSpeed { get; private set; }
        [SerializeField]
        public float MaxMovementSpeed;
        
        [SerializeField]
        public int TargetPriority;

        #endregion

        #region Functions

        protected bool TakeDamage(int damage)
        {
            Health -= damage;

            bool stillAlive = Health <= 0;

            if (!stillAlive) {
                StartCoroutine(Die());
                TargetPriority = -1;
            }

            // Maybe trigger an event here, if we want to react to this somehow (Scavanger idea)

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

        public void Awake()
        {
            Health = MaxHealth;
            MovementSpeed = MaxMovementSpeed;
        }

        protected abstract void OnDeathEffect();

        #endregion
    }
}
