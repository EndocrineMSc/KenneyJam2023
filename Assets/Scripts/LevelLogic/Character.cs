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

        [SerializeField] protected MobDataObject _mobData;
        internal int Cost;
        internal int TargetPriority;
        internal int MaxHealth;
        internal int Health { get; private protected set; }
        private float _maxMovementSpeed = 2;
        internal float MovementSpeed { get; private protected set; }
        internal bool LightFadesOnDeath;
        public Mob MobEnumEntry;

        protected Light2D _light;
        protected readonly float _waitTillDeathTime = 2;
        protected Animator _animator;
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
            //Spawn Sound
            AudioManager.Instance.PlaySFX("Spawn");
            _animator = GetComponent<Animator>();
        }

        protected void InitializeData()
        {
            if (_mobData != null)
            {
                Cost = _mobData.Cost;
                TargetPriority = _mobData.TargetPriority;
                MaxHealth = _mobData.MaxHealth;
                Health = MaxHealth;
                _maxMovementSpeed *= _mobData.MovementSpeedMultiplier;
                MovementSpeed = _maxMovementSpeed;
                LightFadesOnDeath = _mobData.LightFadesOnDeath;
            }
        }

        internal virtual bool TakeDamage(int damage)
        {
            SubtractDamage(damage);
            OnDamageEffect();

            if (_animator != null)
                _animator.SetTrigger("Hit");

            // Towers may decide to change target only when the target died
            return CheckForDeath();
        }

        protected virtual void SubtractDamage(int damage)
        {
            Health -= damage;
        }

        protected bool CheckForDeath()
        {
            bool stillAlive = Health > 0;

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

                if (_animator != null)
                    _animator.SetTrigger("Death");
            }
            MovementSpeed = 0;
            TargetPriority = -1;
            HandleDeathLight();
            FadeSprite();
            yield return new WaitForSeconds(_waitTillDeathTime);

            //Death Sound
            AudioManager.Instance.PlaySFX("Death");

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

        internal void Heal(int amount)
        {
            Health += amount;

            if (Health > MaxHealth)
                Health = MaxHealth;

             StartCoroutine(BlinkGreen()); 
        }

        protected IEnumerator BlinkGreen()
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.color = Color.green;
            yield return new WaitForSeconds(0.3f);
            renderer.color = Color.white;
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
