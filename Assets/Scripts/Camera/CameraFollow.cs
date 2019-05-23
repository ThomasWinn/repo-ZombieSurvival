﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // target for camera to follow
    public float smoothing = 5f;

    Vector3 offset; // distance from camera and player

    void Start()
    {
    	offset = transform.position - target.position;
    }

    void FixedUpdate() 
    {
    	Vector3 targetCamPos = target.position + offset;
    	transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
