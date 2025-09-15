using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;

namespace Statuses
{
    public class StatStatus : BaseStatus
    {
        [SerializeField, ValueDropdown("GetStatNames")]
        private string statName;

        [SerializeField] private Modifier modifier;

        protected override void OnInitialized()
        {
        }

        protected override void OnBegin()
        {
            Target.StatCollection.AddModifier(this.statName, this.modifier);
        }

        protected override void OnEnd()
        {
            Target.StatCollection.RemoveModifier(this.statName, this.modifier);
        }

        protected override IEnumerator OnExecuting()
        {
            yield return null;
        }

#if UNITY_EDITOR
        private IEnumerable<string> GetStatNames()
        {
            FieldInfo[] fieldInfos = typeof(StatId).GetFields(BindingFlags.Static | BindingFlags.Public); // lay toan bo Field static, public trong class StatId cho vao mot list
            var statNames = new List<string>();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                statNames.Add(fieldInfo.Name); //add vao thanh list string statNames
            }

            return statNames;
        }
#endif
    }
}