using System.Collections;
using System.Threading.Tasks;
using Actors;
using Alvin.TowerDefense.Combat;
using MyPooler;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Abilities
{
    public class ShootAbility : BaseAbility
    {
        [SerializeField] private GameObject bulletPrefab;
        [ShowInInspector, ReadOnly] private Actor castle;
        private ActorFactory actorFactory;
        
        private async void Start()
        {
            await Task.Delay(500);
            actorFactory = GameObject.Find("ActorFactory").GetComponent<ActorFactory>();
            this.castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Actor>();
        }

        protected override IEnumerator OnExecute()
        {
            if (Owner.Vision.TryGetClosestFrom(this.castle, out Actor target))
            {
                if (target != null)
                {
                    Shoot(target);
                }
            }
            yield break;
        }

        private void Shoot(Actor shootTarget)
        {
            Vector3 spawnPos = Owner.transform.position + new Vector3(0, 5f, 0);
            //Actor bullet = actorFactory.SpawnActor(bulletPrefab, spawnPos, Owner.transform.rotation);
            
            GameObject bulletGo = ObjectPooler.Instance.GetFromPool(bulletPrefab.tag, spawnPos, Owner.transform.rotation);
            Actor bullet = bulletGo.GetComponent<Actor>();
            actorFactory.AddStats(bullet);
            if (bullet.GetComponent<KamikazeState>())
            {
                bullet.GetComponent<KamikazeState>().Target = shootTarget;
            }
            if (bullet.GetComponent<CannonTravelState>())
            {
                bullet.GetComponent<CannonTravelState>().Target = shootTarget;
            }
        }
    }
}