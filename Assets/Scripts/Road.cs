using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public RoadManager roadManager;
    
    [HideInInspector] public Vector3 endPoint,startPoint;
    
    [SerializeField] private GameSettingsSO gameSettings;
    [SerializeField] private Transform endTransform,startTransform;
    private void Start()
    {
        endPoint = endTransform.position;
        var dir = (startTransform.position - endPoint).normalized;
        startPoint = startTransform.position - dir * gameSettings.gridSize / 2; // Calculate new start point as joint point
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(endTransform.position,Vector3.one/5);
        Gizmos.DrawSphere(startPoint,.2f);
    }
}
