using System;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace UI_Scripts
{
    public class UIController : MonoBehaviour
    {
        
        [Header("----- UI EXP Elements -----")]
        [SerializeField] private Slider expLevelSlider;
        [SerializeField] private TMP_Text expLevelText;
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private TMP_Text timerText;
        
        [Header("----- UI Panel Elements -----")]
        public LevelUpSelectionButton[] levelUpButtons;
        public GameObject levelUpPanel;
        public GameObject levelEndPanel;
        public GameObject pausePanel;
        public TMP_Text levelEndText;
        public string mainMenuName;
        public AudioSource bgmAudio;
        public PlayerStatUpgradeDisplay 
            moveSpeedUpgradeDisplay,
            healthUpgradeDisplay,
            pickupRangeUpgradeDisplay,
            maxWeaponsUpgradeDisplay;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseUnpause();
        }

        public void UpdateExperience(int currentExp,int levelExp,int currentLevel)
        {
            expLevelSlider.value = currentExp;
            expLevelSlider.maxValue = levelExp;
            expLevelText.text = $"Level: {currentLevel}";
        }

        public void UpdateCoins(int coinAmount)
        {
            coinText.text = $"Coin: {coinAmount}";
        }
        
        public void SkipLevelUp()
        {
            levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        

        public void UpdateTimer(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}"; 
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(mainMenuName);
            Time.timeScale = 1f;
        }
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
            Time.timeScale = 1f;
        }

        public void PauseUnpause()
        {
            if (!pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
                bgmAudio.Pause();
                
            }
            else
            {
                pausePanel.SetActive(false);
                bgmAudio.Play();
                if (!levelUpPanel.activeSelf)
                    Time.timeScale = 1f;            
            }
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
        
        public void PurchaseHealth()
        {
            PlayerStatController.Instance.PurchaseHealth();
            //SkipLevelUp();
        }

        public void PurchaseMoveSpeed()
        {
            PlayerStatController.Instance.PurchaseMoveSpeed();
            //SkipLevelUp();
        }

        public void PurchasePickUpRange()
        {
            PlayerStatController.Instance.PurchasePickUpRange();
            //SkipLevelUp();
        }

        public void PurchaseMaxWeapons()
        {
            PlayerStatController.Instance.PurchaseMaxWeapons(); 
            //SkipLevelUp();
        }
    }
}