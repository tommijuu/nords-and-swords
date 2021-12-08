﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform camTransform;

    public float shakeDuration = 0f;

    public float shakeAmount = 0.2f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;


    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
