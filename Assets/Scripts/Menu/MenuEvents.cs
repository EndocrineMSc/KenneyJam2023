using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class MenuEvents
    {
        public static event Action OnSettingsOpened;
        public static event Action OnCreditsOpened;
        public static event Action OnHowToPlayOpened;
        public static event Action OnMainMenuOpened;


        public static void RaiseSettingsOpened()
        {
            OnSettingsOpened?.Invoke();
        }

        public static void RaiseCreditsOpened()
        {
            OnCreditsOpened?.Invoke();
        }

        public static void RaiseHowToPlayOpened()
        { 
            OnHowToPlayOpened?.Invoke();
        }

        public static void RaiseMainMenuOpened()
        {
            OnMainMenuOpened?.Invoke();
        }
    }
}
