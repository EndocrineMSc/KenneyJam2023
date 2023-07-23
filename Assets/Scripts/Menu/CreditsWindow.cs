using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class CreditsWindow : MonoBehaviour
{
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _menuCanvas.enabled = false;
        _backButton.onClick.AddListener(BackButtonClick);
    }

    private void OnEnable()
    {
        MenuEvents.OnSettingsOpened += OnOtherMenuOpened;
        MenuEvents.OnHowToPlayOpened += OnOtherMenuOpened;
        MenuEvents.OnCreditsOpened += OnMenuOpened;
        MenuEvents.OnMainMenuOpened += OnOtherMenuOpened;
    }

    private void OnDisable()
    {
        MenuEvents.OnSettingsOpened -= OnOtherMenuOpened;
        MenuEvents.OnHowToPlayOpened -= OnOtherMenuOpened;
        MenuEvents.OnCreditsOpened -= OnMenuOpened;
        MenuEvents.OnMainMenuOpened -= OnOtherMenuOpened;
    }

    private void OnOtherMenuOpened()
    {
        _menuCanvas.enabled = false;
    }

    private void OnMenuOpened()
    {
        _menuCanvas.enabled = true;
    }

    public void BackButtonClick()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        MenuEvents.RaiseMainMenuOpened();
    }
}
