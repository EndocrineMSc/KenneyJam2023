using Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(menuName = "Tower Collection")]
    internal class CharacterCollection : ScriptableObject
    {
        internal Dictionary<Turret, GameObject> AllTurrets { get; private set; }

        [SerializeField] private List<Turret> _sortedEnum;
        [SerializeField] private List<GameObject> _characterList;

        private void OnValidate()
        {
            _sortedEnum ??= new();
            _characterList ??= new();
            AllTurrets ??= new();

            _sortedEnum = Enum.GetValues(typeof(Turret)).Cast<Turret>().ToList();
            _sortedEnum.Sort();
            _characterList = _characterList.OrderBy(go => go.name).ToList();

            int charIndex = 0;
            for (int enumIndex = 0; enumIndex < _sortedEnum.Count; enumIndex++)
            {
                if (charIndex < _characterList.Count && _sortedEnum[enumIndex] == _characterList[charIndex].GetComponent<Tower>().TurretName)
                {
                    AllTurrets.Add(_sortedEnum[enumIndex], _characterList[charIndex]);
                    charIndex++;
                }
            }
        }
    }

    internal enum Turret
    {
        Arrow,
        MG,
        Bombs,
        Frost,
        Sniper,
        Net,
        Napalm
    }
}
