using System;
using System.Collections.Generic;
using Actors;
using UnityEngine;

namespace Visions
{
    public abstract class BaseVision : MonoBehaviour
    {
        public abstract event Action<Actor> OnEnemyFound;
        
        public abstract bool TryGetClosestFrom(Actor center, out Actor target);
        public abstract List<Actor> GetTargetInRange();
    }
}