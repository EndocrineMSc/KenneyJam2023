using System;
using UnityEngine;

namespace Characters
{
    internal class CharacterEvents
    {
        #region Event Declarations

        internal static event Action<GameObject> OnCharacterReachedGoal;
        internal static event Action<GameObject> OnCharacterDeath;

        #endregion

        #region Event Functions

        internal static void RaiseReachedGoal(GameObject characterObject)
        {
            OnCharacterReachedGoal?.Invoke(characterObject);
        }

        internal static void RaiseDeath(GameObject characterObject)
        {
            OnCharacterDeath?.Invoke(characterObject);
        }

        #endregion
    }
}
