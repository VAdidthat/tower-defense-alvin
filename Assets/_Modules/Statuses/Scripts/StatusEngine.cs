using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Statuses
{
    public class StatusEngine : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] private Actor actor;
        [ShowInInspector, ReadOnly] private List<BaseStatus> statuses = new List<BaseStatus>();

        public void Initialize(Actor actor)
        {
            this.actor = actor;
        }

        public void AddStatus(GameObject statusPrefab)
        {
            GameObject statusGo = Instantiate(statusPrefab, transform);
            BaseStatus status = statusGo.GetComponent<BaseStatus>();
            status.Initialize(this.actor, statusPrefab);
            status.OnStatusEnd += StatusEndHandler;
            
            if (!status.IsStackable)
            {
                // remove previously added unstackable status
                BaseStatus currentStatus = this.statuses.FirstOrDefault(x => x.Source == status.Source);
                if (currentStatus != null)
                {
                    currentStatus.End();
                }
            }
            else
            {
                // reset duration of every status stackable has been added
                var sameSourceStatuses = this.statuses.Where(x => x.Source != null && x.Source == status.Source).ToList();
                foreach (var _status in sameSourceStatuses)
                {
                    _status.ResetTimer();
                }
            }
            
            status.Begin();
            this.statuses.Add(status);
        }

        private void StatusEndHandler(BaseStatus status)
        {
            this.statuses.Remove(status);
        }
    }
}