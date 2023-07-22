using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Characters
{
    internal class BuyButtonToolTip : MonoBehaviour
    {
        #region Fields and Properties

        [SerializeField] private TextMeshProUGUI _mobDescription;
        private RectTransform _tooltipTransform;

        #endregion

        private void Awake()
        {
            _tooltipTransform = GetComponent<RectTransform>();
            ShowTooltip("Test string");
        }

        private void Update()
        {           
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
            transform.localPosition = localPoint;            
        }

        private void ShowTooltip(string description)
        {
            gameObject.SetActive(true);

            _mobDescription.text = description;
            float textPaddingSize = 4f;
            Vector2 backgroundSize = new Vector2(_mobDescription.preferredWidth + textPaddingSize * 2, _mobDescription.preferredHeight + textPaddingSize * 2);
            _tooltipTransform.sizeDelta = backgroundSize;
        }

        internal void SetUp(string description)
        {
            _mobDescription.text = description;
        }
    }
}
