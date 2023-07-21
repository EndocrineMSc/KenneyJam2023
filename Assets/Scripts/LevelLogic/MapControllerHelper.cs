using Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapControllerHelper : MonoBehaviour
{
    /*
     * Returns a list of all characters in range
     */
    public List<Character> FindCharactersInRange(Vector3 origin,
                                                 int range)
    {
        List<Character> loadedCharacters = null; // TODO: Get them from a list or the scene directly

        return loadedCharacters.FindAll(character => GetDistance(origin, character.transform.position) <= range);
     }

    public Character FindClosestCharacter(Vector3 origin, int range)
    {
        float lowestDistance = int.MaxValue;
        Character closestCharacter = null;

        foreach (Character character in FindCharactersInRange(origin, range))
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

    public float GetDistance(Vector3 origin, Vector3 targer)
    {
        return Vector3.Distance(origin, targer);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
