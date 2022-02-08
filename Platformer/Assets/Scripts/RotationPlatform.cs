using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RotationPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 25; // value for speed of rotation
    private float angle = 0; // value includes data about angle of rotation
    private int dir = 0; // direction of rotation
    private void Start()
    {
        angle = transform.localEulerAngles.z;
    }

    private void Update()
    {
        angle = transform.localEulerAngles.z;
        if ((angle - 270) <= 1) 
            dir = 1;
        if ((360 - angle) <= 1)
            dir = -1;

        transform.Rotate(0, 0, (Time.deltaTime * dir * speed), Space.Self);

    }
}
