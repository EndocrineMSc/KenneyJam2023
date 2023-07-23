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
        private float _scoreModifier = 0;

        private void OnEnable()
        {
            CharacterEvents.OnCharacterReachedGoal += CheckForLoss;
            CharacterEvents.OnCharacterReachedGoal += ReduceGoalCounterWrap;
            CharacterEvents.OnCharacterDeath += CheckForLoss;
            UtilityEvents.OnGameWon += GameWon;
            UtilityEvents.OnGameLost += GameLost;
        }

        private void OnDisable()
        {
            CharacterEvents.OnCharacterReachedGoal -= CheckForLoss;
            CharacterEvents.OnCharacterReachedGoal -= ReduceGoalCounterWrap;
            CharacterEvents.OnCharacterDeath -= CheckForLoss;
            UtilityEvents.OnGameWon -= GameWon;
            UtilityEvents.OnGameLost -= GameLost;
        }

        private void CheckForWinWrap()
        {
            if (PlayerData.CheckForPlayerWin())
                UtilityEvents.RaiseGameWon();
        }

        private void CheckForLoss(GameObject go)
        {
            if (PlayerData.Currency < 4 && CharacterSpawner.Instance.ActiveCharacters.Count == 0)
                UtilityEvents.RaiseGameLost();
        }

        private void GameWon()
        {
            PlayerData.SetScore(CalculateScore());
            StartCoroutine(LoadWithFade(SceneName.Victory));
        }

        private void GameLost()
        {
            StartCoroutine(LoadWithFade(SceneName.GameOver));
        }

        private void ReduceGoalCounterWrap(GameObject go)
        {
            PlayerData.ReduceGoalCounter();
            CheckForWinWrap();
        }

        private IEnumerator LoadWithFade(SceneName sceneName)
        {
            _fadeImage.DOFade(1, 1);
            yield return new WaitForSeconds(1);
            LoadHelper.LoadSceneWithLoadingScreen(sceneName);
        }

        private void Update()
        {
            _scoreModifier += Time.deltaTime;
        }

        private int CalculateScore()
        {
            var currency = PlayerData.Currency;
            var modifier = 1 / _scoreModifier;

            var score = Mathf.RoundToInt((currency * 1000 + 10000) * modifier);
            return score;
        }
    }
}
