using DropScripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI_Scripts
{
    public class PlayerStatUpgradeDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText, costText;
        [SerializeField] private GameObject purchaseButton;

        private DropController _dropController;
        
        [Inject]
        public void Construct(DropController dropController)
        {
            _dropController = dropController;
        }

        public void SetDisplay(int cost,float oldValue,float newValue)
        {
            valueText.text = "Value: " + oldValue.ToString("F1") + "->" + newValue.ToString("F1");
            costText.text = "Cost: " + cost;

            if (cost <= _dropController.currentCoins)
                purchaseButton.gameObject.SetActive(true);
            else
                purchaseButton.gameObject.SetActive(false);
        }

        public void ShowMaxDisplay()
        {
            valueText.text = "Max Level";
            costText.text = "Max Level";
            purchaseButton.gameObject.SetActive(false);
        }
    }
}