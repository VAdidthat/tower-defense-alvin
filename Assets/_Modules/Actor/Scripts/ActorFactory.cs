using System;
using Actors;
using Stats;
using UnityEngine;
public class ActorFactory : MonoBehaviour
{
    private Actor actor;
    public Actor SpawnActor(GameObject actorPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject actorGo = Instantiate(actorPrefab, position, rotation);
        this.actor = actorGo.GetComponent<Actor>();
        AddStats(actor);
        return actor;
    }

    public void AddStats(Actor _actor)
    {
        if (_actor.StatCollection.CheckStatEmpty())
        {
            if (_actor.StartingStatsCollection == null)
            {
                AddBasicStats(_actor);
            }
            else
            {
                AddCustomStats(_actor);
            }
        }
    }

    private void AddCustomStats(Actor _actor)
    {
        _actor.StatCollection.AddStat(StatId.Health, new Stat(actor.StartingStatsCollection.Health));
        _actor.StatCollection.AddStat(StatId.Damage, new Stat(actor.StartingStatsCollection.Damage));
        _actor.StatCollection.AddStat(StatId.AttackSpeed, new Stat(actor.StartingStatsCollection.AttackSpeed));
        _actor.StatCollection.AddStat(StatId.MovementSpeed, new Stat(actor.StartingStatsCollection.MovementSpeed));
        _actor.StatCollection.AddStat(StatId.Armor, new Stat(actor.StartingStatsCollection.Armor));
        _actor.StatCollection.AddStat(StatId.MagicResistance, new Stat(actor.StartingStatsCollection.MagicResistance));
        _actor.StatCollection.AddStat(StatId.Price, new Stat(actor.StartingStatsCollection.Price));
    }    private void AddBasicStats(Actor _actor)
    {
        _actor.StatCollection.AddStat(StatId.Health, new Stat(100));
        _actor.StatCollection.AddStat(StatId.Damage, new Stat(10));
        _actor.StatCollection.AddStat(StatId.AttackSpeed, new Stat(1));
        _actor.StatCollection.AddStat(StatId.MovementSpeed, new Stat(5));
        _actor.StatCollection.AddStat(StatId.Armor, new Stat(10));
        _actor.StatCollection.AddStat(StatId.MagicResistance, new Stat(10));
        _actor.StatCollection.AddStat(StatId.Price, new Stat(100));
    }
    private void PrintAllStats()
    {
        Debug.Log($"{actor.gameObject.name} has {actor.StatCollection.GetCurrentValue(StatId.Health)} health, " +
                  $"{actor.StatCollection.GetCurrentValue(StatId.Damage)} damage, " +
                  $"{actor.StatCollection.GetCurrentValue(StatId.MovementSpeed)} movementSpeed, " +
                  $"{actor.StatCollection.GetCurrentValue(StatId.AttackSpeed)} attackSpeed");
    }
}
