using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private List<Level> levels;
    [SerializeField] private Transform spawnPos;

    private int carCount;

    private void OnEnable()
    {
        gameManager.onCarExit += OnCarExit;
        gameManager.onNewLevelStart += OnNewLevelStart;
    }

    private void OnDisable()
    {
        gameManager.onCarExit -= OnCarExit;
        gameManager.onNewLevelStart -= OnNewLevelStart;
    }

    private void OnCarExit()
    {
        carCount--;
        if(carCount <= 0)
            gameManager.onLevelCompleted?.Invoke();
    }

    private void OnNewLevelStart()
    {
        if(spawnPos.childCount > 0 ) Destroy(spawnPos.GetChild(0));
        var nextLevelIndex = gameManager.level % levels.Capacity;
        var nextLevel = levels[nextLevelIndex];
        Instantiate(nextLevel, spawnPos);
        carCount = nextLevel.carCount;
    }
}
