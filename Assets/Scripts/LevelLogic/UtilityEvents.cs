using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    internal class UtilityEvents : MonoBehaviour
    {
        internal static event Action OnGameWon;
        internal static event Action OnGameLost;

        internal static void RaiseGameWon()
        {
            OnGameWon?.Invoke();
        }

        internal static void RaiseGameLost()
        { 
            OnGameLost?.Invoke();
        }
    }
}
