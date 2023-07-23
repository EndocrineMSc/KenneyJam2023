using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class FadeImage : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;


    private void Awake()
    {
        _fadeImage.enabled = true;
        _fadeImage.DOFade(0, 1);
    }
}
