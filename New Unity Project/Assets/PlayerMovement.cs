using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;



    public LeanTweenType easeType;
    Vector3 nextPlace;
    bool moved = false;
    private Animator animator;
    private bool isHoping;

    Vector3 rayOffset = new Vector3(2, 0, 2);

    Vector3 currentDiretcion = new Vector3(0, 0, 0);

    void Start()
    {
        
        Vector3 nextPlace = new Vector3(0, 0, 0);
        Application.targetFrameRate = 70;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        

        if      (Input.GetKeyDown(KeyCode.UpArrow)   || Input.GetKeyDown("w") && !isHoping) { nextPlace = transform.position + new Vector3(4,0,0);  moved = true; }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a") && !isHoping) { nextPlace = transform.position + new Vector3(0,0,4);  moved = true; }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s") && !isHoping) { nextPlace = transform.position + new Vector3(-4,0,0); moved = true; }
        else if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown("d") && !isHoping) { nextPlace = transform.position + new Vector3(0,0,-4); moved = true; }

        if (nextPlace.x<= maxX && nextPlace.x>=0 &&nextPlace.z<= maxZ && nextPlace.z>=0 && moved && !isHoping)
        {
            currentDiretcion = (nextPlace - transform.position).normalized;
            LeanTween.move(gameObject, nextPlace, 0.28f);
            animator.SetTrigger("hop");
            isHoping = true;
           // transform.position = nextPlace;
            moved = false;
           
        }

        var fwd = transform.TransformDirection(Vector3.forward);
        var Reach   = 4.0f;

        Debug.DrawRay(transform.position+ 2*currentDiretcion, currentDiretcion * Reach, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 2 * currentDiretcion, currentDiretcion * Reach, out hit))
        {
            if (hit.transform.tag == "tile")
            {
                Debug.Log("Udarili smo u " + hit.transform.tag + " at position " +hit.transform.position);
            }
        }

        // Debug.Log(transform.position);
    }


    public void FinishedHop()
    {
        isHoping = false;
    }
}
