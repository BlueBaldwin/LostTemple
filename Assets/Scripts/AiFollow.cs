using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFollow : MonoBehaviour
{
    [SerializeField] Transform target; 
    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + _offset;
        transform.LookAt(Camera.main.transform);
    }
}
