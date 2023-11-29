using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/CarSettingsSO", fileName = "CarSettingsSO")]
public class CarSettingsSO : ScriptableObject
{
    public float speed;
    public float movementDuration;
    public float onHitShakeDuration;
    public float onHitShakeMultiplier;
}
