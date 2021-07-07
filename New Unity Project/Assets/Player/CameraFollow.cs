using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 20f;
    public Vector3 offset;

    public float minZoom = 1;
    public float maxZoom = 20;
    public float zoomSpeed = 4f;

    private float currentZoom = 10;
    public float yawSpeed = 100;
    private float currentYaw = 0f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset*currentZoom;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        //transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
