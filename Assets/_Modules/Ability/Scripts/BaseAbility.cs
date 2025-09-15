using System.Collections;
using Actors;
using UnityEngine;

namespace Abilities
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        public float Cooldown => this.cooldown;
        private Coroutine castCoroutine;

        public bool IsExecuting { private set; get; }
        public bool IsAvailable { private set; get; }
        public float RemaningCooldown { private set; get; }
        protected Actor Owner { private set; get; }

        public void Execute(Actor actor)
        {
            if (IsExecuting || !IsAvailable) return;
            Owner = actor;
            IsAvailable = false;
            RemaningCooldown = this.cooldown;
            this.castCoroutine = StartCoroutine(ExecuteCoroutine());
        }

        public void Stop()
        {
            if (this.castCoroutine != null)
            {
                IsExecuting = false;
                StopCoroutine(this.castCoroutine);
            }
        }

        private void Update()
        {
            if (!IsAvailable)
            {
                RemaningCooldown -= Time.deltaTime;

                if (RemaningCooldown <= 0f)
                {
                    IsAvailable = true;
                    RemaningCooldown = 0f;
                }
            }
        }

        private IEnumerator ExecuteCoroutine()
        {
            IsExecuting = true;
            yield return StartCoroutine(OnExecute());
            IsExecuting = false;
        }

        protected abstract IEnumerator OnExecute();
    }
}