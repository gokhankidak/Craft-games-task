using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        AnimateHit();
    }

    private void AnimateHit()
    {
        var localAngles = transform.localEulerAngles;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotate(localAngles - Vector3.right * 15, .15f).SetEase(Ease.InBounce))
            .Append(transform.DORotate(localAngles, .15f).SetEase(Ease.InBounce));
    }
}
