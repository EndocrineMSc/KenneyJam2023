using System;

namespace Characters
{
    internal class CharacterEvents
    {
        #region Event Declarations

        internal static event Action OnCharacterReachedGoal;
        internal static event Action OnCharacterDeath;

        #endregion

        #region Event Functions

        internal static void RaiseReachedGoal()
        {
            OnCharacterReachedGoal?.Invoke();
        }

        internal static void RaiseDeath()
        {
            OnCharacterDeath?.Invoke();
        }

        #endregion
    }
}
