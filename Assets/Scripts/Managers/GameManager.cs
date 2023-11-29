using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

    private void Start()
    {
        gameManager.onNewLevelStart?.Invoke();
    }

    private void OnEnable()
    {
        gameManager.onLevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        gameManager.onLevelCompleted -= OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        gameManager.level++;
        Debug.Log("Level Completed");
    }
}
