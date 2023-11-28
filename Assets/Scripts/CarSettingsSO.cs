using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/CarSettingsSO", fileName = "CarSettingsSO")]
public class CarSettingsSO : ScriptableObject
{
    public float speed;
    [FormerlySerializedAs("goBackDuration")] public float movementDuration;
}
