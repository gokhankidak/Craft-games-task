using System;
using UnityEngine;

public class CarInputController : MonoBehaviour
{
    private CarMovementController movementController;
    private Vector3 touchStartPos;
    private bool isSwiping;
    private  float swipeThreshold = 10f;

    private void Awake()
    {
        movementController = GetComponent<CarMovementController>();
    }

    private void OnMouseDown()
    {
        touchStartPos = Input.mousePosition;
        isSwiping = true;
    }

    private void OnMouseOver()
    {
        if (isSwiping)
        {
            if (IsAboveThreshold(Input.mousePosition))
            {
                isSwiping = false;
                var moveDir = GetMoveDirection(Input.mousePosition);
                movementController.MoveCar(moveDir);
            }
        }
    }

    private void OnMouseUp()
    {
        isSwiping = false;
    }

    private Vector2Int GetMoveDirection(Vector3 mousePos)
    {
        var xDif = Mathf.Abs(touchStartPos.x - mousePos.x);
        var yDif = Mathf.Abs(touchStartPos.y - mousePos.y);

        if (xDif > yDif)
        {
            return touchStartPos.x > mousePos.x ? Vector2Int.left : Vector2Int.right;
        }
        return touchStartPos.y > mousePos.y ? Vector2Int.down : Vector2Int.up;
    }

    private bool IsAboveThreshold(Vector3 mousePos)
    {
        if (Mathf.Abs(touchStartPos.x - mousePos.x) > swipeThreshold) return true;
        if (Mathf.Abs(touchStartPos.y - mousePos.y) > swipeThreshold) return true;

        return false;
    }
}
