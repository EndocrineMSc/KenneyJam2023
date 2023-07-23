using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters
{
    public class MobCollection : MonoBehaviour
    {
        public static MobCollection Instance { get; private set; }
        public Dictionary<Mob, GameObject> AllMobs { get; private set; }

        [SerializeField] private List<Mob> _sortedEnum;
        [SerializeField] private List<GameObject> _characterList;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            _sortedEnum ??= new();
            _characterList ??= new();
            AllMobs ??= new();

            _sortedEnum = _sortedEnum.OrderBy(x => x.ToString()).ToList();
            _characterList = _characterList.OrderBy(go => go.GetComponent<Character>().MobEnumEntry.ToString()).ToList();

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
}
