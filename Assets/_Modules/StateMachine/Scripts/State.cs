using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public abstract class State : MonoBehaviour
    {
        protected StateMachine Machine { private set; get; }

        public void OnStateEnter(StateMachine stateMachine)
        {
            // Code placed here will always run
            Machine = stateMachine;
            Debug.Log($"Actor {Machine.name} entered state {GetType().Name}");
            OnEnter();
        }

        protected virtual void OnEnter()
        {
            // Code placed here can be overridden
        }

        public void OnStateUpdate()
        {
            // Code placed here will always run
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            // Code placed here can be overridden
        }

        public void OnStateExit()
        {
            // Code placed here will always run
            Debug.Log($"Actor {Machine.name} exited state {GetType().Name}");
            OnExit();
        }

        protected virtual void OnExit()
        {
            // Code placed here can be overridden
        }


    }
}