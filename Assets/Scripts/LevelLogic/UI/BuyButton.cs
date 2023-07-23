using Characters;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utility
{
    internal class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private MobDataObject _mobData;
        [SerializeField] private TextMeshProUGUI _mobName;
        [SerializeField] private TextMeshProUGUI _mobCost;
        [SerializeField] private Image _mobImage;
        private Button _button;
        private bool _isWiggling;
        private RectTransform _buttonTransform;

        private void OnValidate()
        {
            InitializeButtonData();
        }

        private void Awake()
        {
            InitializeButtonData();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(TryBuyMob);
            _buttonTransform = GetComponent<RectTransform>();
        }

        private void InitializeButtonData()
        {
            _mobName.text = _mobData.MobName;
            _mobCost.text = "Cost: " + _mobData.Cost.ToString();
            _mobImage.sprite = _mobData.MobSprite;
        }

        private void TryBuyMob()
        {
            var enoughCurrency = PlayerData.CheckForCurrency(_mobData.Cost);

            if (enoughCurrency)
            {
                CharacterSpawner.Instance.SpawnMob(_mobData.MobEnumEntry);
                PlayerData.SpendCurrency(_mobData.Cost);
            }
            else
            {
                //ToDo: Play error sound
            }

        }

        private void Update()
        {
            _button.interactable = PlayerData.CheckForCurrency(_mobData.Cost);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isWiggling)
                StartCoroutine(Wiggle());

            Tooltip.Instance.ShowTooltip(_mobData.TooltipDescription);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.Instance.HideTooltip();
        }

        //ButtonClick
        public void ButtonClick()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            MenuEvents.RaiseMainMenuOpened();
        }

        private IEnumerator Wiggle()
        {
            _isWiggling = true;
            _buttonTransform.DOBlendablePunchRotation(new(0,0,-3), 0.5f, 1, 1);
            yield return new WaitForSeconds(0.2f);
            _isWiggling = false;
        }
    }
}
