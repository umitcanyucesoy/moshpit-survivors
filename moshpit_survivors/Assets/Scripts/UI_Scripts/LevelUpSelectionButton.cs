using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using TMPro;
using UI_Scripts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Weapons;

public class LevelUpSelectionButton : MonoBehaviour
{
    
    [SerializeField] private TMP_Text upgradeDescText,nameLevelText;
    [SerializeField] private Image weaponIcon;

    private WeaponService _assignedWeapon;
    private UIController _uiController;
    private PlayerController _playerController;

    [Inject]
    public void Construct(UIController uiController,PlayerController playerController)
    {
        _uiController = uiController;
        _playerController = playerController;
    }

    public void UpdateButtonDisplay(WeaponService theWeapon)
    {
        if (theWeapon.gameObject.activeSelf)
        {
            upgradeDescText.text = theWeapon.weaponStats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;
        
            nameLevelText.text = $"{theWeapon.name} - Level {theWeapon.weaponLevel + 1}";
        }
        else
        {
            upgradeDescText.text = $"Unlock {theWeapon.name}";
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        
        _assignedWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if (_assignedWeapon == null) return;

        if (_assignedWeapon.gameObject.activeSelf)
            _assignedWeapon.WeaponStatsChange();
        else
            _playerController.PlayerAddWeapon(_assignedWeapon);
        
        
        _uiController.levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
