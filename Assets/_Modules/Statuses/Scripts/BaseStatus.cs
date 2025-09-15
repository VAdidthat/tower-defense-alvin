using System;
using System.Collections;
using Actors;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = System.Object;

namespace Statuses
{
    public abstract class BaseStatus : MonoBehaviour
    {
        [SerializeField, MinValue(0f)] private float duration;
        [SerializeField, Range(0f, 1f)] private float chances = 1f;
        [SerializeField] private bool isStackable;

        public Actor Target { get; private set; }
        public bool IsExecuting { get; private set; }
        public bool IsStackable => this.isStackable;
        public float Chance => this.chances;
        public object Source { get; private set; }

        private float timer;
        private Coroutine timerCoroutine;

        public event Action<BaseStatus> OnStatusEnd;

        public void Initialize(Actor actor, object source)
        {
            Target = actor;
            Source = source;
            OnInitialized();
        }

        public void Begin()
        {
            if (IsExecuting) return;
            this.timerCoroutine = StartCoroutine(ExecuteCoroutine());
        }

        public void End()
        {
            if (!IsExecuting) return;
            StopCoroutine(this.timerCoroutine);
            Clear();
        }

        public void Clear()
        {
            OnStatusEnd?.Invoke(this); // remove statuses
            OnEnd(); // remove modifier / changing state to starting state
            Destroy(this.gameObject);
        }

        public void ResetTimer()
        {
            this.timer = this.duration;
        }

        private IEnumerator ExecuteCoroutine()
        {
            IsExecuting = true;
            this.timer = this.duration;
            OnBegin(); // stat status: add moddifier. stunt status: changing state to deep sleep state

            while (this.timer > 0f)
            {
                this.timer -= Time.deltaTime;

                if (this.timer <= 0f)
                {
                    this.timer = 0f;
                }

                yield return null;
            }

            IsExecuting = false;
            Clear();
        }

        protected abstract void OnInitialized();
        protected abstract void OnBegin();
        protected abstract void OnEnd();
        protected abstract IEnumerator OnExecuting();
    }
}