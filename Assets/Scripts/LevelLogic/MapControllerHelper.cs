using Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Towers;
using UnityEngine;
using UnityEngine.UIElements;

internal static class MapControllerHelper
{
    /*
     * Returns a list of all characters in range
     */
    internal static List<GameObject> FindCharactersInRange(Vector3 origin, int range)
    {
        List<GameObject> loadedCharacters = CharacterSpawner.Instance.ActiveCharacters;

        return loadedCharacters.FindAll(character => GetDistance(origin, character.transform.position) <= range);
     }

    internal static GameObject FindFurthestAdvancedCharacterInRange(Vector2 origin, int range) {

        List<Character> charactersInRange = new();

        return FindCharactersInRange(origin,range).
            OrderBy(character => character.GetComponent<Character>()).FirstOrDefault();
    }

    internal static GameObject FindHighestPriorityInRange(Vector2 origin, int range) { 

        return FindCharactersInRange(origin, range).
                OrderBy(character => character.GetComponent<Character>().TargetPriority).FirstOrDefault();
    }

    internal static GameObject FindClosestCharacterInRange(Vector3 origin, int range)
    {
        float lowestDistance = int.MaxValue;
        GameObject closestCharacter = null;

        foreach (GameObject character in FindCharactersInRange(origin, range))
        {
            float currentDistance = GetDistance(origin, character.transform.position);
            if (currentDistance < lowestDistance)
            {
                lowestDistance = currentDistance;
                closestCharacter = character;
            }
        }

        return closestCharacter;    
    }

    internal static float GetDistance(Vector3 origin, Vector3 targer)
    {
        return Vector3.Distance(origin, targer);
    }

}
