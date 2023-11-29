using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private List<Road> roads;
    [SerializeField] private RoadSettingsSO roadSettings;
    [SerializeField] private float durationMultiplier = 3;
    
    /// <summary>
    /// Road take all car movement control
    /// </summary>
    public void MoveAlongRoadPath(Road road,Vector3 hitPos,Transform car)
    {
        var roadIndex = roads.IndexOf(road);
        var closestPoint = GetClosestPointOnLine(road.startPoint, road.endPoint, hitPos);
        var sequence = DOTween.Sequence();
        var rotateDuration = roadSettings.rotateDuration;
        var singleGridDuration = roadSettings.moveSingleGridDuration;
        var yOffset = car.position.y * Vector3.up;
        
        //add sequence to hit to end point
        sequence.Append(car.DOMove(closestPoint + yOffset , singleGridDuration))
            .Join(car.DORotate(road.transform.eulerAngles, rotateDuration))
            .Append(car.DOMove(road.endPoint + yOffset , singleGridDuration * road.transform.lossyScale.z * durationMultiplier));
        
        for (int i = roadIndex + 1; i < roads.Count; i++)
        {
            var currentRoad = roads[i];
            
            sequence.Append(car.DOMove(currentRoad.startPoint + yOffset , singleGridDuration))
                .Join(car.DORotate(currentRoad.transform.eulerAngles, rotateDuration));
                
            sequence.Append(car.DOMove(currentRoad.endPoint + yOffset  , singleGridDuration * currentRoad.transform.lossyScale.z * durationMultiplier));
        }
    }
    
    Vector3 GetClosestPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 lineDirection = lineEnd - lineStart;
        float lineLength = lineDirection.magnitude;
        lineDirection.Normalize();

        Vector3 pointDirection = point - lineStart;
        float dotProduct = Vector3.Dot(pointDirection, lineDirection);
        dotProduct = Mathf.Clamp(dotProduct, 0f, lineLength);

        return lineStart + lineDirection * dotProduct;
    }
}
