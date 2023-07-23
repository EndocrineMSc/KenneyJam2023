using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Utility
{
    public class ScoreDisplay : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var textField = GetComponent<TextMeshProUGUI>();
            textField.text = "Your score: " + PlayerData.Score.ToString();
        }
    }
}
