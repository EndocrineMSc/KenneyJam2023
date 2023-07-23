using Characters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

        private float BuyCooldown;

        private void OnValidate()
        {
            InitializeButtonData();
        }

        private void Awake()
        {
            InitializeButtonData();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(TryBuyMob);

            BuyCooldown = Time.time;
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
                _button.interactable = false;
                BuyCooldown = Time.time + (_mobData.Cost / 5);
            }
            else
            {
                //ToDo: Play error sound
            }

        }

        private void Update()
        {
            if(Time.time > BuyCooldown)
                _button.interactable = PlayerData.CheckForCurrency(_mobData.Cost);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
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
    }
}
