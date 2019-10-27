using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Vernon
/// 2018.05.28
/// </summary>
public class CameraFollow : MonoBehaviour {
    private Transform target;

    public Vector2 distance;
    public float downAngle;
    public float smoothTime;

    private Vector3 offset;

    private void Start () {
        
	}

    private void FixedUpdate()
    {
        Follow();
    }

    //Follow target
    private void Follow()
    {
        //Set Camera position
        Vector3 forward = target.forward * distance.x;

        Vector3 up = target.up * distance.y;

        offset = target.position + forward + up;

        offset = Vector3.Lerp(transform.position, offset, smoothTime * Time.deltaTime);

        //Set camera rotatoin
        Vector3 currentAngle =  target.localEulerAngles;

        Vector3 targetAngle = new Vector3(currentAngle.x + downAngle, currentAngle.y, currentAngle.z);

        Quaternion targetRotation = Quaternion.Euler(targetAngle);

        transform.SetPositionAndRotation(offset, targetRotation);
        
    }

    //Set target for the camera
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
