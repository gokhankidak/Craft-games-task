using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [HideInInspector] public Vector3 endPoint,startPoint;
    [SerializeField] private GameSettingsSO gameSettings;
    
    public RoadManager roadManager;

    [SerializeField] private CarSettingsSO settingsSo;
    [SerializeField] private Transform endTransform,startTransform;
    private void Start()
    {
        endPoint = endTransform.position;
        startPoint = startTransform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(endTransform.position,Vector3.one/5);
        Gizmos.DrawSphere(startPoint,.2f);
    }
}
