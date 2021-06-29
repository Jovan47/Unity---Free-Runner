using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 maxPlace;
    Vector3 minPlace;
    Vector3 nextPlace;
    bool moved = false;
   
    void Start()
    {
        maxPlace = new Vector3(16, 0, 16);
        minPlace = new Vector3 (0, 0, 0);
        Vector3 nextPlace = new Vector3(0, 0, 0);
        Application.targetFrameRate = 70;
    }

    void Update()
    {
        

        if      (Input.GetKeyDown(KeyCode.UpArrow)   || Input.GetKeyDown("w")) { nextPlace = transform.position + new Vector3(4,0,0);  moved = true; }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a")) { nextPlace = transform.position + new Vector3(0,0,4);  moved = true; }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s")) { nextPlace = transform.position + new Vector3(-4,0,0); moved = true; }
        else if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown("d")) { nextPlace = transform.position + new Vector3(0,0,-4); moved = true; }

        if (nextPlace.x<=36 &&nextPlace.x>=0 &&nextPlace.z<=36 &&nextPlace.z>=0 && moved)
        {
            transform.position = nextPlace;
            moved = false;
        }

        Debug.Log(transform.position);
    }
}
