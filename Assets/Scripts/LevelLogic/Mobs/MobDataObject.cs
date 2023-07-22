using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu (menuName = "Mob Data Object")]
    public class MobDataObject : ScriptableObject
    {
        public int Cost;
        public int TargetPriority;
        public int MaxHealth;
        public float MovementSpeedMultiplier;
        public bool LightFadesOnDeath;
        public Mob MobEnumEntry;
        public string MobName;
        public Sprite MobSprite;
        [TextArea] public string TooltipDescription;
    }
}
