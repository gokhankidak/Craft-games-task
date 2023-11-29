using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameManagerSO", fileName = "GameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    public Action onCarExit;
    public Action onLevelCompleted;
    public Action onNewLevelStart;

    public int level;
}
