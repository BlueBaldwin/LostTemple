using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPuzzleTrigger : MonoBehaviour
{
    [SerializeField] Collider _puzzleTrigger;
    private void OnTriggerEnter(Collider other)
    {
        _puzzleTrigger.GetComponent<Collider>().enabled = true;
    }
}
