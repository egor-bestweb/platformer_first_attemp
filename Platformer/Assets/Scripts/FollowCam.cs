using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float SmoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 tagetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, tagetPos, ref _velocity, SmoothTime);


    }


}

