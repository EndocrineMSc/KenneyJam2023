using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    internal class CharacterSpawner : MonoBehaviour
    {
        #region Fields and Properties

        internal static CharacterSpawner Instance { get; private set; }
        internal List<GameObject> ActiveCharacters { get; private set; }

        [SerializeField] private CharacterCollection _mobCollection;
        private Dictionary<Mob, GameObject> _allMobs;
        [SerializeField] private GameObject _spawnPoint;

        #endregion

        #region Functions

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            ActiveCharacters ??= new();
            _allMobs = _mobCollection.AllMobs;
        }

        private void OnEnable()
        {
            CharacterEvents.OnCharacterDeath += RemoveMobFromActiveList;
            CharacterEvents.OnCharacterReachedGoal += RemoveMobFromActiveList;
        }

        private void OnDisable()
        {
            CharacterEvents.OnCharacterDeath -= RemoveMobFromActiveList;
            CharacterEvents.OnCharacterReachedGoal -= RemoveMobFromActiveList;
        }

        internal void SpawnMob(Mob mob)
        {
            var mobObject = Instantiate(_allMobs[mob], _spawnPoint.transform.position, Quaternion.identity);
            ActiveCharacters.Add(mobObject);
        }

        private void RemoveMobFromActiveList(GameObject characterObject)
        {
            if (ActiveCharacters.Contains(characterObject))
                ActiveCharacters.Remove(characterObject);
        }

        public void TestSpawn()
        {
            SpawnMob(Mob.Lantern);
        }

        #endregion
    }
}
