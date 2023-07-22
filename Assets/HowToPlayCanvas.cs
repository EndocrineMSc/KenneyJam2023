using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class HowToPlayCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _menuCanvas;

    private void Awake()
    {
        _menuCanvas.enabled = false;
    }

    private void OnEnable()
    {
        MenuEvents.OnSettingsOpened += OnOtherMenuOpened;
        MenuEvents.OnHowToPlayOpened += OnMenuOpened;
        MenuEvents.OnCreditsOpened += OnOtherMenuOpened;
        MenuEvents.OnMainMenuOpened += OnOtherMenuOpened;
    }

    private void OnDisable()
    {
        MenuEvents.OnSettingsOpened -= OnOtherMenuOpened;
        MenuEvents.OnHowToPlayOpened -= OnMenuOpened;
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
}
