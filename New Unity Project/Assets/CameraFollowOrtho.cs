using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowOrtho : MonoBehaviour
{
    public Transform target;
    public Transform offset;

    private void Update()
    {
        transform.position = target.position + offset.position;
    }

}
