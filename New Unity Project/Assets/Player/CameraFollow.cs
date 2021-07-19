using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 20f;
    public Vector3 offset;
    public Vector3 rotationOffset;
    public float minZoom = 1;
    public float maxZoom = 20;
    public float zoomSpeed = 3.5f;

    private float currentZoom = 10;
    public float yawSpeed = 100;
    private float currentYaw = 0f;
    private float timer = 0;
    private bool lookAtPlayerSwitcher = true;
    private float rotationSpeed=0.1f;

    private Quaternion beforeRotation;
    private int  flagCount = 0;
    private bool flagSwitcher = false;
    public bool isTweenComplete = true;
    private float count=0;
    private void Start()
    {
      //  transform.LookAt(target);


    }

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;

        if (!PauseMenu.isPaused)
        {
            LeanTween.cancel(gameObject);
            isTweenComplete = true;
            Vector3 desiredPosition = target.position + offset * currentZoom;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        //transform.RotateAround(target.position, Vector3.up, currentYaw);
        if (PauseMenu.isPaused)
        {
            if (flagCount == 0)
            {
                flagCount++;
                beforeRotation = transform.rotation;
            }
            RotateCameraWhenPaused();
        }
        else if(!PauseMenu.isPaused && flagCount != 0)
        {
            
            transform.rotation = beforeRotation;
            flagCount = 0;
        }
        
    }

    public void RotateCameraWhenPaused()
    {
        //Vector3 pos =transform.position = target.position + new Vector3(0, 3, -10);
    
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.unscaledTime);
        if (isTweenComplete)
        {
            isTweenComplete = false;
            LeanTween.moveY(gameObject, 5f, 1f).setLoopPingPong();
        }
    }

    public void TweenCallback()
    {
        isTweenComplete = true;
    }
}
