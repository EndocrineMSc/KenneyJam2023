using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class PlayAgain : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    public void PlayAgainClicked()
    {
        StartCoroutine(LoadWithFade(SceneName.Menu));
    }

    private IEnumerator LoadWithFade(SceneName sceneName)
    {
        _fadeImage.DOFade(1, 1);       
        yield return new WaitForSeconds(1);
        LoadHelper.LoadSceneWithLoadingScreen(sceneName);
    }
}
