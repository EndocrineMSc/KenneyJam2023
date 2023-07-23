using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    internal class LoadHelper
    {
        internal static SceneName SceneToBeLoaded { get; private set; } = SceneName.Menu;
        internal static float LoadDuration { get; private set; } = 1f;

        internal static void LoadSceneWithLoadingScreen(SceneName sceneName)
        {
            SceneToBeLoaded = sceneName;
            SceneManager.LoadSceneAsync(SceneName.LoadingScreen.ToString());
        }
    }

    internal enum SceneName
    {
        Menu,
        LoadingScreen,
        ProductionLevel,
        Victory,
        GameOver,
    }
}
