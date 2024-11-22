using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI_Scripts;
using UnityEngine;
using VContainer;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public float timer;
    
    private UIController _uiController;
    private bool _gameActive;
    
    
    [Inject]
    public void Construct(UIController uiController)
    {
        _uiController = uiController;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!_gameActive) return;
        timer += Time.deltaTime;
        _uiController.UpdateTimer(timer);
    }
    
    private void StartGame()
    {
        _gameActive = true;
        timer = 0f;
    }
    
    public void EndGame()
    {
        _gameActive = false;
        _ = EndGameAfterAnimation();
    }
    
    private async Task EndGameAfterAnimation()
    {
        const float animationLength = 1.5f;
        await Task.Delay(TimeSpan.FromSeconds(animationLength));
        var minutes = Mathf.FloorToInt(timer / 60);
        var seconds = Mathf.FloorToInt(timer % 60);
        _uiController.levelEndText.text = minutes.ToString("00") + " mins " + seconds.ToString("00" + " secs");
        _uiController.levelEndPanel.SetActive(true);
    }
}
