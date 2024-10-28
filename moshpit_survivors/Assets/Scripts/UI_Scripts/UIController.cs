using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class UIController : MonoBehaviour
    {
        
        [Header("----- UI EXP Elements -----")]
        [SerializeField] private Slider expLevelSlider;
        [SerializeField] private TMP_Text expLevelText;
        
        public LevelUpSelectionButton[] levelUpButtons;
        public GameObject levelUpPanel;

        public void UpdateExperience(int currentExp,int levelExp,int currentLevel)
        {
            expLevelSlider.value = currentExp;
            expLevelSlider.maxValue = levelExp;
            expLevelText.text = $"Level: {currentLevel}";
        }

        public void SkipLevelUp()
        {
            levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}