using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Characters
{
    [CreateAssetMenu(menuName = "Mob Collection")]
    internal class CharacterCollection : ScriptableObject
    {
        internal Dictionary<Mob, GameObject> AllMobs { get; private set; }
        
        [SerializeField] private List<Mob> _sortedEnum;
        [SerializeField] private List<GameObject> _characterList;

        private void OnValidate()
        {
            _sortedEnum ??= new();
            _characterList ??= new();
            AllMobs ??= new();

            _sortedEnum = Enum.GetValues(typeof(Mob)).Cast<Mob>().ToList();
            _sortedEnum.Sort();
            _characterList = _characterList.OrderBy(go => go.name).ToList();

            int charIndex = 0;
            for (int enumIndex = 0; enumIndex < _sortedEnum.Count; enumIndex++)
            {
                if (charIndex < _characterList.Count && _sortedEnum[enumIndex] == _characterList[charIndex].GetComponent<Character>().MobEnumEntry)
                {
                    AllMobs.Add(_sortedEnum[enumIndex], _characterList[charIndex]);
                    charIndex++;
                }
            }
        }
    }

    public enum Mob
    {
        Lantern,
        Tank,
        Swarm,
        Scavenger,
        Rogue,
        Sprinter,
        Stacked,
        Shield,
        Healer,
    }
}
