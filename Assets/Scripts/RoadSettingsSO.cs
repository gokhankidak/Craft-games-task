using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RoadSettings", fileName = "RoadSettingsSO")]
public class RoadSettingsSO : ScriptableObject
{
    public float rotateDuration;
    public float moveSingleGridDuration;
}
