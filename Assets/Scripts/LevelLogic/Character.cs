using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    internal abstract class Character : MonoBehaviour
    {
        #region Fields and Properties

        public int Cost;
        public int TargetPriority;
        public int MaxHealth;
        public int Health { get; private protected set; }
        private float _maxMovementSpeed = 4;
        public float MovementSpeed { get; private protected set; }
        public float MovementSpeedMultiplier;
        public bool LightFadesOnDeath;

        protected Light2D _light;
        protected readonly float _waitTillDeathTime = 2;
        #endregion

        #region Functions

        protected virtual void Awake()
        {
            Health = MaxHealth;
            _maxMovementSpeed *= MovementSpeedMultiplier;
            MovementSpeed = _maxMovementSpeed;
            _light = GetComponentInChildren<Light2D>();
        }

        protected bool TakeDamage(int damage)
        {
            SubtractDamage(damage);
            OnDamageEffect();

            // Towers may decide to change target only when the target died
            return CheckForDeath();
        }

        protected virtual void SubtractDamage(int damage)
        {
            Health -= damage;
        }

        protected bool CheckForDeath()
        {
            bool stillAlive = Health <= 0;

            if (!stillAlive)
            {
                StartCoroutine(Die());
                TargetPriority = -1;
            }

            return stillAlive;
        }

        protected IEnumerator Die()
        {           
            CharacterEvents.RaiseDeath();
            HandleDeathLight();
            FadeSprite();
            OnDeathEffect();
            yield return new WaitForSeconds(_waitTillDeathTime);
            
            StopAllCoroutines();
            Destroy(gameObject);
        }

        internal void SlowCharacter(float slowAmount)
        {
            // Do this multiplicative? Reduce by % of max speed
            StartCoroutine(RestoreOriginalSpeed(_maxMovementSpeed));
            MovementSpeed -= slowAmount;
            if (MovementSpeed < 0)
                MovementSpeed = 0;
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

        protected void FadeSprite()
        {
            SpriteRenderer characterSprite = GetComponent<SpriteRenderer>();
            characterSprite.DOFade(0, (_waitTillDeathTime / 2));
        }

        protected void HandleDeathLight()
        {
            if (LightFadesOnDeath)
                StartCoroutine(FadeLight());
            else
                _light.intensity = 0;
        }

        protected IEnumerator FadeLight()
        {
            float elapsedTime = 0;

            while (elapsedTime < _waitTillDeathTime)
            {
                _light.intensity = Mathf.Lerp(_light.intensity, 0, (elapsedTime / _waitTillDeathTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _light.intensity = 0;
            yield return null;
        }


        protected abstract void OnDeathEffect();

        protected abstract void OnDamageEffect();


        #endregion
    }
}
