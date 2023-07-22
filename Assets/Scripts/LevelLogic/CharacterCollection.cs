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
            _sortedEnum = _sortedEnum.OrderBy(x => x.ToString()).ToList();
            _characterList = _characterList.OrderBy(go => go.GetComponent<Character>().MobEnumEntry).ToList();

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
        Healer,
        Lantern,
        Rogue,
        Scavenger,
        Shield,
        Sprinter,
        Stacked,
        Swarm,
        Tank,
    }
}
