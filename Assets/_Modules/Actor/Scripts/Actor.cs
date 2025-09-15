using Alvin.TowerDefense.Combat;
using Sirenix.OdinInspector;
using Stats;
using Statuses;
using TypeReferences;
using UnityEngine;
using Visions;

namespace Actors
{
    [RequireComponent(typeof(StateMachine))]
    public class Actor : MonoBehaviour
    {
        [SerializeField, ClassExtends(typeof(State))]
        private ClassTypeReference startingState;
        public ClassTypeReference StartingState => startingState;
        [ShowInInspector, ReadOnly] public StatCollection StatCollection { private set; get; }
        public StartingStatsCollection StartingStatsCollection;
        public StateMachine StateMachine { private set; get; }
        public BaseVision Vision { private set; get; }
        public Vector3 Position => this.trans.position;
        public Health Health { private set; get; }
        public StatusEngine StatusEngine { private set; get; }

        [SerializeField] private Transform graphicTransform;

        private Transform trans;

        private void Awake()
        {
            this.trans = transform;
            StatCollection = new StatCollection();
            Health = GetComponent<Health>();
            Vision = GetComponent<BaseVision>();
            
            StateMachine = GetComponent<StateMachine>();
            StateMachine.Initialize();
            StateMachine.ChangeState(this.startingState);
            
            StatusEngine = GetComponent<StatusEngine>();
            StatusEngine.Initialize(this);
        }

        public void SetRotation(Quaternion rotation)
        {
            this.graphicTransform.rotation = rotation;
        }

        public void SetPosition(Vector3 position)
        {
            this.trans.position = position;
        }
    }
}