using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target = null;

    private Vector3 offset;
    bool flag = false;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, 0, target.position.z)+offset, Time.deltaTime*3);

       
        Debug.Log(Mathf.Abs(transform.position.y-target.position.y));
        
        if(Mathf.Abs(transform.position.y - target.position.y) > 5)
        {
            offset.y += target.position.y;
        }

    }
}
