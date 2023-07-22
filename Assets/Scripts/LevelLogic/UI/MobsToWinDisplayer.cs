using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Utility
{
    internal class MobsToWinDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mobsAmount;
        private RectTransform _mobsTextTransform;
        private int _lastUpdateAmount;
        private bool _isTweening;

        private void Awake()
        {
            _lastUpdateAmount = PlayerData.MobGoalCounter;
            _mobsTextTransform = _mobsAmount.GetComponent<RectTransform>();
        }


        // Update is called once per frame
        void Update()
        {
            _mobsAmount.text = PlayerData.MobGoalCounter.ToString();

            if (_lastUpdateAmount != PlayerData.MobGoalCounter && !_isTweening)
                StartCoroutine(TweenTextChange());

            _lastUpdateAmount = PlayerData.MobGoalCounter;
        }

        private IEnumerator TweenTextChange()
        {
            _isTweening = true;
            _mobsTextTransform.DOPunchScale(_mobsTextTransform.localScale * 1.2f, 0.3f, 1, 1);
            _mobsTextTransform.DOBlendablePunchRotation(_mobsTextTransform.localEulerAngles * 1.2f, 0.3f, 1, 1); 
            yield return new WaitForSeconds(0.31f);
            _isTweening = false;
        }
    }
}
