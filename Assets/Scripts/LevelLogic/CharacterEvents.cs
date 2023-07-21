using System;

namespace Characters
{
    internal class CharacterEvents
    {
        #region Event Declarations

        internal static event Action OnCharacterReachedGoal;

        #endregion

        #region Event Functions

        internal static void RaiseReachedGoal()
        {
            OnCharacterReachedGoal?.Invoke();
        }

        #endregion
    }
}
