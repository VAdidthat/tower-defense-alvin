using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Stats
{
    public class StatCollection
    {
        [ShowInInspector] private readonly Dictionary<string, Stat> stats;

        public StatCollection()
        {
            stats = new Dictionary<string, Stat>();
        }

        public bool CheckStatEmpty()
        {
            if (stats.Count == 0)
            {
                return true;
            }
            return false;
        }
        public float GetCurrentValue(string statName)
        {
            return stats[statName].CurrentValue;
        }

        public void AddStat(string statName, Stat stat)
        {
            stats.Add(statName, stat);
        }

        public void RemoveStat(string statName)
        {
            stats.Remove(statName);
        }

        public void AddModifier(string statName, Modifier modifier)
        {
            stats[statName].AddModifier(modifier);
        }

        public void RemoveModifier(string statName, Modifier modifier)
        {
            stats[statName].RemoveModifier(modifier);
        }
    }
}