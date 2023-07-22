using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class SettingMenu : MonoBehaviour
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
        MenuEvents.OnSettingsOpened += OnMenuOpened;
        MenuEvents.OnHowToPlayOpened += OnOtherMenuOpened;
        MenuEvents.OnCreditsOpened += OnOtherMenuOpened;
        MenuEvents.OnMainMenuOpened += OnOtherMenuOpened;
    }

    private void OnDisable()
    {
        MenuEvents.OnSettingsOpened -= OnMenuOpened;
        MenuEvents.OnHowToPlayOpened -= OnOtherMenuOpened;
        MenuEvents.OnCreditsOpened -= OnOtherMenuOpened;
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
        MenuEvents.RaiseMainMenuOpened();
    }
}
