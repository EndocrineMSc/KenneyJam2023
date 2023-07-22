using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu (menuName = "Mob Data Object")]
    internal class MobDataObject : ScriptableObject
    {
        internal int Cost;
        internal int TargetPriority;
        internal int MaxHealth;
        internal float MovementSpeedMultiplier;
        internal bool LightFadesOnDeath;
        internal Mob MobEnumEntry;
        internal string MobName;
        internal Sprite MobSprite;
    }
}
