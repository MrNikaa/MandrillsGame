using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime;
    public Transform target;

    public Vector3 offSet;

    private void LateUpdate()
    {
        Vector3 desired = target.position + offSet;
        Vector3 smoothed = Vector3.Lerp(transform.position, desired, smoothTime * Time.deltaTime);

        transform.position = smoothed;
    }

}
