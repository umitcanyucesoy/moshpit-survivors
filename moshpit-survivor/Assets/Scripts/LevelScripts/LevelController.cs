using System;
using System.Collections.Generic;
using System.IO.Pipes;
using PlayerScripts;
using SFX;
using UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;
using Weapons;
using Random = UnityEngine.Random;

namespace LevelScripts
{
    public class LevelController : MonoBehaviour
    {
        [Header("-----EXPERIENCE OBJECT-----")]
        [SerializeField] private GameObject pickup;
        [SerializeField] private Transform parent;
        [Header("-----EXPERIENCE SETTINGS-----")]
        [SerializeField] private List<int> expLevels;
        [SerializeField] private int currentExperience;
        [SerializeField] private int currentLevel = 1,levelCount = 100;

        [Header("----- LEVEL WEAPONS -----")]
        public List<WeaponService> weaponsToUpgrade;


        private IObjectResolver _resolver;
        private ExperiencePickup _experiencePickup;
        private UIController _uiController;
        private PlayerController _playerController;
        

        [Inject]
        public void Construct(IObjectResolver resolver,UIController uiController,PlayerController playerController)
        {
            _resolver = resolver;
            _uiController = uiController;
            _playerController = playerController;
            
        }

        private void Awake()
        {
            _experiencePickup = pickup.GetComponent<ExperiencePickup>();
        }

        private void Start()
        {
            AddLevel();
        }

        public void GetExp(int amountToGetExp)
        {
            currentExperience += amountToGetExp;

            if (currentExperience >= expLevels[currentLevel])
            {
                LevelUp();
            }
            
            _uiController.UpdateExperience(currentExperience, expLevels[currentLevel],currentLevel);
            SFXManager.instance.PlaySfxPitched(2);
        }

        public void SpawnExp(Vector3 position,int expValue)
        {
            var pickups =_resolver.Instantiate(pickup,position,Quaternion.identity);
            pickups.transform.SetParent(parent);
            _experiencePickup.expValue = expValue;
        }

        private void AddLevel()
        {
            while (expLevels.Count < levelCount)
            {
                expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
            }
        }

        private void LevelUp()
        {
            currentExperience -= expLevels[currentLevel];
            currentLevel++;

            if (currentLevel >= expLevels.Count)
            {
                currentLevel = expLevels.Count - 1;
            }
            
            _uiController.levelUpPanel.gameObject.SetActive(true);
            PlayerStatController.Instance.UpdateDisplay();
            Time.timeScale = 0f;                        
            
            weaponsToUpgrade.Clear();
            var availableWeapons = new List<WeaponService>();
            availableWeapons.AddRange(_playerController.assignedWeapons);

            if (availableWeapons.Count > 0)
            {
                var selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
            
            if(_playerController.assignedWeapons.Count + _playerController.fullyLevelledWeapons.Count < _playerController.maxWeapons)
                availableWeapons.AddRange(_playerController.unassignedWeapons);

            for (var i = weaponsToUpgrade.Count; i < 3; i++)
            {
                if (availableWeapons.Count > 0)
                {
                    var selected = Random.Range(0, availableWeapons.Count);
                    weaponsToUpgrade.Add(availableWeapons[selected]);
                    availableWeapons.RemoveAt(selected);
                }
            }
            
            for (var i = 0; i < weaponsToUpgrade.Count; i++)
                _uiController.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);

            for (var i = 0; i < _uiController.levelUpButtons.Length; i++)
            {
                if (i < weaponsToUpgrade.Count)
                {
                    _uiController.levelUpButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    _uiController.levelUpButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
