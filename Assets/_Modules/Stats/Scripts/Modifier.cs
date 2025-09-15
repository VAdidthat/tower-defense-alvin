using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Modifier
    {
        [SerializeField] private float value;
        [SerializeField] private ModifierType type;

        public float Value => this.value;

        public ModifierType Type => this.type;
        // type 0,1,2 tuong ung flat, percent, more,..

        public Modifier(float value, ModifierType type)
        {
            this.value = value;
            this.type = type;
        }
    }
}