using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    internal class WinLoseConditionManager : MonoBehaviour
    {
        private void OnEnable()
        {
            CharacterEvents.OnCharacterReachedGoal += CheckForWinWrap;
            CharacterEvents.OnCharacterReachedGoal += CheckForLoss;
            CharacterEvents.OnCharacterDeath += CheckForLoss;
        }

        private void OnDisable()
        {
            CharacterEvents.OnCharacterReachedGoal -= CheckForWinWrap;
            CharacterEvents.OnCharacterReachedGoal -= CheckForLoss;
            CharacterEvents.OnCharacterDeath -= CheckForLoss;
        }

        private void CheckForWinWrap(GameObject go)
        {
            if (PlayerData.CheckForPlayerWin())
                UtilityEvents.RaiseGameWon();
        }

        private void CheckForLoss(GameObject go)
        {
            if (PlayerData.Currency < 4)
                UtilityEvents.RaiseGameLost();
        }
    }
}
