using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor;


[RequireComponent(typeof(CarInputController))]
public class CarMovementController : MonoBehaviour
{
    [SerializeField] private Vector2Int carSize;
    [SerializeField] private CarSettingsSO carSettings;
    [SerializeField] private GameSettingsSO gameSettings;

    private float gridSize, moveSpeed;
    private bool isMoving,hasExited,isVertical;
    private Vector2Int globalCarSize;
    private Vector3 centerAndFirstGridDif;
    private Vector3 moveDir = Vector3.zero;
    

    private void Start()
    {
        globalCarSize = transform.eulerAngles.y % 180 == 0 ? carSize : new Vector2Int(carSize.y, carSize.x);//car size according to rotation
        centerAndFirstGridDif = new Vector3((globalCarSize.x - 1) / 2f,0,(globalCarSize.y - 1) / 2f);//first grid and transform center position difference
        
        moveSpeed = carSettings.speed;
        isVertical = transform.eulerAngles.y % 180 != 0;
        gridSize = gameSettings.gridSize;
    }
    
    public void MoveCar(Vector2Int dir)
    {
        if (isMoving) return;
        if(dir.x == 0 && isVertical) return;//check movement direction and car direction are not same
        if(dir.y == 0 && !isVertical) return;
        
        moveDir = new Vector3(dir.x, 0, dir.y);
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if(!isMoving || hasExited) return;
        MoveCarOnGrid();
    }

    private void MoveCarOnGrid()
    {
        var pos = transform.position;
        var movePos = new Vector3(pos.x + moveDir.x * moveSpeed, pos.y, pos.z + moveDir.z * moveSpeed);
        transform.position = movePos;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Road") && !hasExited)
        {
            var road = other.GetComponent<Road>();
            OnExitPark(road);
        }
    }

    private void OnExitPark(Road road)
    {
        hasExited = true;
        isMoving = false;

        DOTween.Kill(this);
        var roadManager = road.roadManager;
        roadManager.MoveAlongRoadPath(road, transform.position, transform);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car") ||
            other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            isMoving = false;
            MoveClosestGrid();
        }
    }

    private void MoveClosestGrid()
    {
        moveDir = Vector3.zero;
        DOTween.Kill(this);
        transform.DOMove(GetClosestGrid(), carSettings.movementDuration);
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
