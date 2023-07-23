using Characters;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    internal class WinLoseConditionManager : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;

        private void OnEnable()
        {
            CharacterEvents.OnCharacterReachedGoal += CheckForWinWrap;
            CharacterEvents.OnCharacterReachedGoal += CheckForLoss;
            CharacterEvents.OnCharacterReachedGoal += ReduceGoalCounterWrap;
            CharacterEvents.OnCharacterDeath += CheckForLoss;
            UtilityEvents.OnGameWon += GameWon;
            UtilityEvents.OnGameLost += GameLost;
        }

        private void OnDisable()
        {
            CharacterEvents.OnCharacterReachedGoal -= CheckForWinWrap;
            CharacterEvents.OnCharacterReachedGoal -= CheckForLoss;
            CharacterEvents.OnCharacterReachedGoal -= ReduceGoalCounterWrap;
            CharacterEvents.OnCharacterDeath -= CheckForLoss;
            UtilityEvents.OnGameWon -= GameWon;
            UtilityEvents.OnGameLost -= GameLost;
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

        private void GameWon()
        {
            StartCoroutine(LoadWithFade(SceneName.Victory));
        }

        private void GameLost()
        {
            StartCoroutine(LoadWithFade(SceneName.GameOver));
        }

        private void ReduceGoalCounterWrap(GameObject go)
        {
            PlayerData.ReduceGoalCounter();
        }

        private IEnumerator LoadWithFade(SceneName sceneName)
        {
            _fadeImage.DOFade(1, 1);
            yield return new WaitForSeconds(1);
            LoadHelper.LoadSceneWithLoadingScreen(sceneName);
        }
    }
}
