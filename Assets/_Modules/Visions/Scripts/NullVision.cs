using System;
using System.Collections.Generic;
using Actors;

namespace Visions
{
    public class NullVision : BaseVision
    {
        public override event Action<Actor> OnEnemyFound
        {
            add { }
            remove { }
        }
        
        public override bool TryGetClosestFrom(Actor center, out Actor target)
        {
            target = null;
            return false;
        }
        
        public override List<Actor> GetTargetInRange()
        {
            return null;
        }
    }
}