using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Stats
{
    /// <summary>
    /// Hp, attack speed, movement speed
    /// </summary>
    public class Stat
    {
        [ShowInInspector, ReadOnly, PropertyOrder(1)] public float BaseValue { private set; get; }

        [ShowInInspector, ReadOnly, PropertyOrder(2)] public float CurrentValue { private set; get; }

        [ShowInInspector, ReadOnly, PropertyOrder(3)] private readonly List<Modifier> modifiers;

        public Stat(float baseValue)
        {
            BaseValue = baseValue;
            CurrentValue = baseValue;
            this.modifiers = new List<Modifier>();
        }

        public void SetBaseValue(float baseValue)
        {
            BaseValue = baseValue;
            CurrentValue = Calculate(BaseValue, this.modifiers);
        }

        public void AddModifier(Modifier modifier)
        {
            this.modifiers.Add(modifier);
            CurrentValue = Calculate(BaseValue, this.modifiers);
        }

        public void RemoveModifier(Modifier modifier)
        {
            this.modifiers.Remove(modifier);
            CurrentValue = Calculate(BaseValue, this.modifiers);
        }

        private static float Calculate(float baseValue, IEnumerable<Modifier> mods)
        {
            float finalValue = baseValue;
            var sumPercentAdd = 0f;
            var sumPercentMulMore = 0f;
            var sumPercentMulLess = 0f;

            foreach (Modifier mod in mods)
            {
                switch (mod.Type)
                {
                    case ModifierType.Flat:
                    {
                        finalValue += mod.Value;
                        break;
                    }
                    case ModifierType.Percent:
                    {
                        sumPercentAdd += mod.Value;
                        break;
                    }
                    case ModifierType.More:
                    {
                        // More
                        if (mod.Value >= 0f)
                        {
                            sumPercentMulMore += mod.Value;
                        }
                        // Less
                        else
                        {
                            sumPercentMulLess += mod.Value;
                        }

                        break;
                    }
                }
            }

            // Percent Add (Increase, Decrease)
            finalValue *= 1f + sumPercentAdd;

            // Percent Mul (More, Less)
            finalValue *= 1f + sumPercentMulMore;
            finalValue *= 1f + sumPercentMulLess;
            
            // final value can not be negative
            if (finalValue < 0f)
            {
                finalValue = 0f;
            }

            return finalValue;
        }
    }
}