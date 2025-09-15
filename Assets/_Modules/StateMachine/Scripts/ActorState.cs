using Actors;

namespace Alvin.TowerDefense.Combat
{
    public class ActorState : State
    {
        private Actor owner;

        protected Actor Owner
        {
            get
            {
                if (this.owner == null)
                {
                    this.owner = GetComponent<Actor>();
                }

                return this.owner;
            }
        }
    }
}