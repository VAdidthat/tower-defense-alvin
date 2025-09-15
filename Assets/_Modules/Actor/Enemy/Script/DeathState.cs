using System;
using Actors;
using Alvin.TowerDefense.Games;
using Stats;
using UnityEngine;

namespace Alvin.TowerDefense.Combat
{
    public class DeathState : ActorState
    {
        protected override void OnEnter()
        {
            if (Owner.tag == "Enemy")
            {
                var inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
                inventory.gold.Increase(Owner.StatCollection.GetCurrentValue(StatId.Price));
            }

            if (Owner.tag == "Castle")
            {
                var game =  GameObject.Find("Game").GetComponent<Game>();
                game.GameOver();
            }
            
            Destroy(gameObject);
        }
    }
}