using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Canvas _menuCanvas;

    private void Awake()
    {
        _howToPlayButton.onClick.AddListener(OpenHowToPlay);
        _settingsButton.onClick.AddListener(OpenSettings);
        _creditsButton.onClick.AddListener(OpenCredits);
    }

    private void OnEnable()
    {
        MenuEvents.OnSettingsOpened += OnOtherMenuOpened;
        MenuEvents.OnHowToPlayOpened += OnOtherMenuOpened;
        MenuEvents.OnCreditsOpened += OnOtherMenuOpened;
        MenuEvents.OnMainMenuOpened += OnMenuOpened;
    }

    private void OnDisable()
    {
        MenuEvents.OnSettingsOpened -= OnOtherMenuOpened;
        MenuEvents.OnHowToPlayOpened -= OnOtherMenuOpened;
        MenuEvents.OnCreditsOpened -= OnOtherMenuOpened;
        MenuEvents.OnMainMenuOpened -= OnMenuOpened;
    }

    public void PlayGame()
    {
        LoadHelper.LoadSceneWithLoadingScreen(SceneName.ProductionLevel);
    }

    private void OnOtherMenuOpened()
    {
        _menuCanvas.enabled = false;
    }

    private void OnMenuOpened()
    {
        _menuCanvas.enabled = true;
    }

    public void OpenSettings()
    {
        MenuEvents.RaiseSettingsOpened();
    }

    public void OpenHowToPlay()
    {
        MenuEvents.RaiseHowToPlayOpened();
    }

    public void OpenCredits()
    {
        MenuEvents.RaiseCreditsOpened();
    }
}
