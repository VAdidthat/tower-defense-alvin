using System;
using Abilities;
using Actors;
using Stats;
using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public class MoveState : ActorState
    {
        [SerializeField] private bool hasHealingAbility;
        [SerializeField] private BaseAbility ability;
        private float moveSpeed;
        [SerializeField] private GameObject[] waypoints;
        private int currentWaypoint;

        void Start()
        {
            GameObject waypointsGo = GameObject.FindGameObjectWithTag("Waypoints");
            waypoints = waypointsGo.GetComponent<Waypoints>().GetWayPoints();
            
            //start at first waypoint and rotate in to move direction
            Owner.SetPosition(waypoints[currentWaypoint].transform.position);
            RotateIntoMoveDirection();
        }

        protected override void OnUpdate()
        {
            //healing ability
            if (hasHealingAbility)
            {
                if (Owner.Health.CurrentHealth < Owner.Health.MaxHealth)
                {
                    this.ability.Execute(Owner);
                }
            }
            
            // get movement speed
            moveSpeed = Machine.GetComponent<Actor>().StatCollection.GetCurrentValue(StatId.MovementSpeed);
            Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
            
            //move
            Vector3 direction = (endPosition - Machine.transform.position).normalized;
            Machine.transform.position += direction * (moveSpeed * Time.deltaTime);
            
            // check point
            if (Vector3.Distance(gameObject.transform.position, endPosition) < 0.1f)
            {
                if (currentWaypoint < waypoints.Length - 2)
                {
                    currentWaypoint++;
                    RotateIntoMoveDirection();
                }
                else
                {
                    Machine.ChangeState<AttackState>();
                }
            }
        }
        private void RotateIntoMoveDirection()
        {
            //1
            Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
            Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
            Vector3 newDirection = (newEndPosition - newStartPosition);
            //2
            float z = newDirection.z;
            float x = newDirection.x;
            float rotationAngle = Mathf.Atan2(x, z) * 180 / Mathf.PI + 180;
            //3
            Owner.SetRotation(Quaternion.AngleAxis(rotationAngle, Vector3.up));
        }
        public float DistanceToFinish()
        {
            float distance = 0;
            distance += Vector3.Distance(Machine.transform.position, waypoints[currentWaypoint + 1].transform.position);
            for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
            {
                distance += Vector3.Distance(waypoints[i].transform.position, waypoints[i + 1].transform.position);
            }
            return distance;
        }

    }
}