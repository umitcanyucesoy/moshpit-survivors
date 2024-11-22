using System;
using System.Collections.Generic;
using Datas;
using DropScripts;
using UI_Scripts;
using UnityEngine;
using VContainer;

namespace PlayerScripts
{
    [Serializable]
    public class PlayerStatValue
    {
        public int cost;
        public float value;

        public PlayerStatValue(int newCost, float newValue)
        {
            cost = newCost;
            value = newValue;
        }
    }
    
    public class PlayerStatController : MonoBehaviour
    {

        public static PlayerStatController Instance;
        private void Awake()
        {
            Instance = this;
        }

        public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapons;
        public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
        [HideInInspector] public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;

        private UIController _uiController;
        private DropController _dropController;
        private PlayerData _playerData;
        private PlayerController _playerController;
        
        [Inject]
        public void Construct(UIController uiController,DropController dropController,PlayerController playerController,PlayerData playerData)
        {
            _uiController = uiController;
            _dropController = dropController;
            _playerController = playerController;
            _playerData = playerData;
        }

        private void Start()
        {
            for (var i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
            {
                moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost,
                    moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
            }
            
            for (var i = health.Count - 1; i < healthLevelCount; i++)
            {
                health.Add(new PlayerStatValue(health[i].cost + health[1].cost,
                    health[i].value + (health[1].value - health[0].value)));
            }
            
            for (var i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
            {
                pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost,
                    pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
            }
        }

        private void Update()
        {
            if (_uiController.levelUpPanel.gameObject.activeSelf)
                UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (moveSpeedLevel < moveSpeedLevelCount - 1)
                _uiController.moveSpeedUpgradeDisplay.SetDisplay(moveSpeed[moveSpeedLevel + 1].cost,
                    moveSpeed[moveSpeedLevel].value,moveSpeed[moveSpeedLevel + 1].value);
            else
                _uiController.moveSpeedUpgradeDisplay.ShowMaxDisplay();
            
            if (healthLevel < healthLevelCount - 1)
                _uiController.healthUpgradeDisplay.SetDisplay(health[healthLevel + 1].cost,
                    health[healthLevel].value,health[healthLevel + 1].value);
            else
                _uiController.healthUpgradeDisplay.ShowMaxDisplay();
            
            if (pickupRangeLevel < pickupRangeLevelCount - 1)
                _uiController.pickupRangeUpgradeDisplay.SetDisplay(pickupRange[pickupRangeLevel + 1].cost,
                    pickupRange[pickupRangeLevel].value,pickupRange[pickupRangeLevel + 1].value);
            else
                _uiController.pickupRangeUpgradeDisplay.ShowMaxDisplay();
            
            if (maxWeaponsLevel < maxWeapons.Count - 1)
                _uiController.maxWeaponsUpgradeDisplay.SetDisplay(maxWeapons[maxWeaponsLevel + 1].cost,
                    maxWeapons[maxWeaponsLevel].value,maxWeapons[maxWeaponsLevel + 1].value);
            else
                _uiController.maxWeaponsUpgradeDisplay.ShowMaxDisplay();
        }

        public void PurchaseHealth()
        {
            healthLevel++;
            _dropController.SpendCoin(health[healthLevel].cost);
            UpdateDisplay();

            _playerData.health = health[healthLevel].value;
            PlayerHealthController.Instance.currentHealth +=
                (health[healthLevel].value - health[healthLevel - 1].value);
        }

        public void PurchaseMoveSpeed()
        {
            moveSpeedLevel++;
            _dropController.SpendCoin(moveSpeed[moveSpeedLevel].cost);
            UpdateDisplay();

            _playerData.moveSpeed = moveSpeed[moveSpeedLevel].value;
        }

        public void PurchasePickUpRange()
        {
            pickupRangeLevel++;
            _dropController.SpendCoin(pickupRange[pickupRangeLevel].cost);
            UpdateDisplay();

            _playerData.pickupRange = pickupRange[pickupRangeLevel].value;
        }

        public void PurchaseMaxWeapons()
        {
            maxWeaponsLevel++;
            _dropController.SpendCoin(maxWeapons[maxWeaponsLevel].cost);
            UpdateDisplay();

            _playerController.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].value);
        }
        
    }
}