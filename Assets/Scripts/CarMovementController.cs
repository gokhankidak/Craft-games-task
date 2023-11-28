using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor;


[RequireComponent(typeof(CarInputController))]
public class CarMovementController : MonoBehaviour
{
    [SerializeField] private Vector2Int carSize;
    [SerializeField] private CarSettingsSO carSettings;
    private Vector2Int globalCarSize;
    private float gridSize = 1f;
    private float moveSpeed = .1f;
    private bool isMoving,hasExited;
    private Vector3 centerAndFirstGridDif;
    private Vector3 moveDir = Vector3.zero;
    

    private void Start()
    {
        globalCarSize = transform.eulerAngles.y % 180 == 0 ? carSize : new Vector2Int(carSize.y, carSize.x);
        centerAndFirstGridDif = new Vector3((globalCarSize.x - 1) / 2f,0,(globalCarSize.y - 1) / 2f);
        moveSpeed = carSettings.speed;
    }
    
    public void MoveCar(Vector2Int dir)
    {
        if (isMoving) return;
        moveDir = new Vector3(dir.x, 0, dir.y);
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if(!isMoving || hasExited) return;

        var pos = transform.position;
        transform.position = new Vector3(pos.x + moveDir.x * moveSpeed, pos.y, pos.z + moveDir.z * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Road") && !hasExited)
        {
            hasExited = true;
            isMoving = false;

            DOTween.Kill(transform);

            var road = other.GetComponent<Road>();
            var roadManager = road.roadManager;
            roadManager.MoveAlongRoadPath(road,transform.position,transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car") ||
            other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            isMoving = false;
            moveDir = Vector3.zero;
            DOTween.Kill(this);
            transform.DOMove(GetClosestGrid(), carSettings.movementDuration);
        }
    }

    private Vector3 GetClosestGrid()
    {
        var firstGridPos = transform.position - centerAndFirstGridDif;
        
        float snappedX = Mathf.Round(firstGridPos.x / gridSize) * gridSize;
        float snappedZ = Mathf.Round(firstGridPos.z / gridSize) * gridSize;

        var snappedPos = new Vector3(snappedX, firstGridPos.y, snappedZ);

        return snappedPos + centerAndFirstGridDif;
    }
}
