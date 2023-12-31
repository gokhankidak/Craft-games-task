using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private GameObject levelCompleteMenu;
    [SerializeField] private GameObject confettiParticle;

    [Header("Parameters")] [SerializeField]
    private float levelCompleteMenuDelay;
    
    private void OnEnable()
    {
        gameManager.onLevelCompleted += OnLevelComplete;
    }

    private void OnDisable()
    {
        gameManager.onLevelCompleted -= OnLevelComplete;
    }
    
    public void GoToNextLevel()
    {
        gameManager.onNewLevelStart?.Invoke();
        levelCompleteMenu.SetActive(false);
        confettiParticle.SetActive(false);
    }

    private async void OnLevelComplete()
    {
        confettiParticle.SetActive(true);
        await Task.Delay((int)(levelCompleteMenuDelay * 1000));
        levelCompleteMenu.SetActive(true);
    }
}
