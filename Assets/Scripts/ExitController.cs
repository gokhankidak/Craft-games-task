using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;

    private void OnTriggerEnter(Collider other)// on car reach exit
    {
        other.gameObject.SetActive(false);
        gameManager.onCarExit?.Invoke();
    }
}
