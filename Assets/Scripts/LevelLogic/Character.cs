using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    internal class Character : MonoBehaviour
    {
        private void OnEnable()
        {
            CharacterEvents.OnCharacterReachedGoal += TestDebug; //only for testing
        }

        private void OnDisable()
        {
            CharacterEvents.OnCharacterReachedGoal -= TestDebug;
        }

        private void TestDebug()
        {
            Debug.Log("Character has arrived at final destination!");
        }
    }
}
