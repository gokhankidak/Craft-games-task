using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float animDuration = .15f;
    private float animStrength = .15f;
    private void OnCollisionEnter(Collision other)
    {
        AnimateHit();
    }

    private void AnimateHit()
    {
        var localAngles = transform.localEulerAngles;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotate(localAngles - Vector3.right * animStrength, animDuration).SetEase(Ease.InBounce))
            .Append(transform.DORotate(localAngles, animDuration).SetEase(Ease.InBounce));
    }
}
