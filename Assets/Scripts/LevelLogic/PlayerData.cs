using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    internal class PlayerData
    {
        #region Fields and Properties

        internal static int Currency { get; private set; } = 200;
        internal static int MobGoalCounter { get; private set; } = 5;
        internal static int Score { get; private set; } = 0;

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

        internal static void AddCurrency(int amount)
        {
            Currency += amount;
        }

        internal static void ReduceGoalCounter() 
        { 
            MobGoalCounter--; 

            if (MobGoalCounter <= 0)
                MobGoalCounter = 0;
        }

        internal static bool CheckForPlayerWin()
        {
            return MobGoalCounter <= 0;
        }

        internal static void SetScore(int score)
        {
            Score = score;
        }

        internal static void ResetState()
        {
            Currency = 200;
            MobGoalCounter = 5;
            Score = 0;
        }

        #endregion
    }
}
