using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    internal class PlayerData
    {
        #region Fields and Properties

        internal static int Currency { get; private set; } = 200;
        internal static int MobGoalCounter { get; private set; } = 0;
        internal static int TargetGoalCounter { get; private set; } = 5;

        #endregion

        #region Functions

        internal static bool CheckForCurrency(int cost)
        {
            return Currency >= cost;
        }

        internal static void SpendCurrency(int amount)
        {
            Currency -= amount;
        }

        internal static void AddToGoalCounter() 
        { 
            MobGoalCounter++; 
        }

        internal static bool CheckForPlayerWin()
        {
            return MobGoalCounter >= TargetGoalCounter;
        }

        #endregion
    }
}
