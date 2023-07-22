using TMPro;
using UnityEngine;

namespace Characters
{
    internal class Tooltip : MonoBehaviour
    {
        #region Fields and Properties

        internal static Tooltip Instance { get; private set; }

        [SerializeField] private RectTransform _canvasTransform;
        [SerializeField] private TextMeshProUGUI _mobDescription;
        [SerializeField] private RectTransform _backgroundTransform;
        private RectTransform _rectTransform;

        #endregion

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            _rectTransform = GetComponent<RectTransform>();
            _backgroundTransform = GetComponent<RectTransform>();
            HideTooltip();
        }

        private void Update()
        {
            Vector2 anchoredPosition = Input.mousePosition / _canvasTransform.localScale.x;

            if (anchoredPosition.x + _rectTransform.rect.width > _canvasTransform.rect.width)
                anchoredPosition.x = _canvasTransform.rect.width - _rectTransform.rect.width;

            if (anchoredPosition.y + _rectTransform.rect.height > _canvasTransform.rect.height)
                anchoredPosition.y = _canvasTransform.rect.height - _rectTransform.rect.height;

            _rectTransform.anchoredPosition = anchoredPosition;
        }

        private void SetText(string description)
        {
            _mobDescription.SetText(description);
            _mobDescription.ForceMeshUpdate();
        }

        internal void ShowTooltip(string description)
        {
            gameObject.SetActive(true);
            SetText(description);
        }

        internal void HideTooltip()
        {
            gameObject.SetActive(false);
        }
    }
}
