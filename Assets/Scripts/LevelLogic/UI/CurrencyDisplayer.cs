using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Utility
{
    internal class CurrencyDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyAmount;
        private RectTransform _currencyTextTransform;
        private int _lastUpdateAmount;
        private bool _isTweening;

        private void Awake()
        {
            _lastUpdateAmount = PlayerData.Currency;
            _currencyTextTransform = _currencyAmount.GetComponent<RectTransform>();
        }


        // Update is called once per frame
        void Update()
        {
            _currencyAmount.text = PlayerData.Currency.ToString();

            if (_lastUpdateAmount != PlayerData.Currency && !_isTweening)
                StartCoroutine(TweenTextChange());

            _lastUpdateAmount = PlayerData.Currency;
        }

        private IEnumerator TweenTextChange()
        {
            _isTweening = true;
            _currencyTextTransform.DOPunchScale(_currencyTextTransform.localScale * 1.2f, 0.3f, 1, 1);
            _currencyTextTransform.DOBlendablePunchRotation(_currencyTextTransform.localEulerAngles * 1.2f, 0.3f, 1, 1); 
            yield return new WaitForSeconds(0.31f);
            _isTweening = false;
        }
    }
}
