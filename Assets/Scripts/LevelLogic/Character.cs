using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class Character : MonoBehaviour
    {
        #region Fields and Properties

        internal MobDataObject MobData;
        internal int Cost;
        internal int TargetPriority;
        internal int MaxHealth;
        internal int Health { get; private protected set; }
        private float _maxMovementSpeed = 4;
        internal float MovementSpeed { get; private protected set; }
        internal float MovementSpeedMultiplier;
        internal bool LightFadesOnDeath;
        internal Mob MobName;

        protected Light2D _light;
        protected readonly float _waitTillDeathTime = 2;
        #endregion

        #region Functions

        protected void OnValidate()
        {
            InitializeData();
        }

        protected virtual void Awake()
        {
            InitializeData();
            _light = GetComponentInChildren<Light2D>();
        }

        protected void InitializeData()
        {
            if (MobData != null)
            {
                Cost = MobData.Cost;
                TargetPriority = MobData.TargetPriority;
                MaxHealth = MobData.MaxHealth;
                Health = MaxHealth;
                _maxMovementSpeed *= MovementSpeedMultiplier;
                MovementSpeed = _maxMovementSpeed;
                LightFadesOnDeath = MobData.LightFadesOnDeath;
            }
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
                StartCoroutine(Die());

            return stillAlive;
        }

        internal IEnumerator Die(bool hasReachedGoal = false)
        {      
            if(!hasReachedGoal)
            {
                CharacterEvents.RaiseDeath(gameObject);
                OnDeathEffect();
            }
            TargetPriority = -1;
            HandleDeathLight();
            FadeSprite();
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
