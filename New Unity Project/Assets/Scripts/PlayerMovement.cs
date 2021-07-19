using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;
    [SerializeField] private float offSetPlayerMove = 4;
    [SerializeField] private float rayReach = 5;
    private bool moved;
    private bool isHoping;
    private Vector3 nextPlace;
    private Vector3 currentDiretcion;
    private Animator animator;
    public GameObject MapSpawner = null;
    private MapSpawner scriptRef = null;
    private bool playerMoved = false;
    public LayerMask Obstacles;
    public RaycastHit[] hits;
    private enum Direction { left, right, down, up };
    Direction direction;
    void Start()
    {
        scriptRef = MapSpawner.GetComponent<MapSpawner>();
        currentDiretcion = new Vector3(0, 0, 0);
        moved = false;
        Vector3 nextPlace = new Vector3(0, 0, 0);
        Application.targetFrameRate = 70;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w") && !isHoping && !PauseMenu.isPaused)
        {
            nextPlace = transform.position + new Vector3(offSetPlayerMove, 0, 0);
            currentDiretcion = ((nextPlace - new Vector3(0, 4, 0)) - nextPlace).normalized;
            moved = true;
            direction = Direction.up;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a") && !isHoping && !PauseMenu.isPaused)
        {
            nextPlace = transform.position + new Vector3(0, 0, offSetPlayerMove);
            currentDiretcion = ((nextPlace - new Vector3(0, 4, 0)) - nextPlace).normalized;
            moved = true;
            direction = Direction.left;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s") && !isHoping && !PauseMenu.isPaused)
        {
            nextPlace = transform.position + new Vector3(-offSetPlayerMove, 0, 0);
            currentDiretcion = ((nextPlace - new Vector3(0, 4, 0)) - nextPlace).normalized;
            moved = true;
            direction = Direction.down;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d") && !isHoping && !PauseMenu.isPaused)
        {
            nextPlace = transform.position + new Vector3(0, 0, -offSetPlayerMove);
            currentDiretcion = ((nextPlace - new Vector3(0, 4, 0)) - nextPlace).normalized;
            moved = true;
            direction = Direction.right;


        }
        //nextPlace.x <= maxX && nextPlace.x >= 0 && nextPlace.z <= maxZ && nextPlace.z >= 0
        if (moved && !isHoping)
        {
            Vector3 dirr = new Vector3(0, 0, 0);
            dirr = ((nextPlace) - nextPlace - new Vector3(0, -1, 0)).normalized;
            Ray ray = new Ray(nextPlace + new Vector3(0, -1, 0), dirr * rayReach);
            hits = Physics.RaycastAll(ray);

            Debug.DrawRay(nextPlace, dirr * rayReach, Color.red);

            bool isObst = false;
            if (hits.Length == 0)
            {

                playerMoved = true;

            }
            foreach (var x in hits)
            {
                // Debug.Log(x.collider.tag);
                if (x.transform.tag == "Obstacle")
                {
                    isObst = true;
                    playerMoved = false;
                }

                if (!isObst)
                {
                    playerMoved = true;
                }
                // else { playerMoved = false; }
            }
            /*
            //Debug.DrawRay(nextPlace+new Vector3(0,2,0), currentDiretcion * rayReach, Color.red);
            if (Physics.Raycast(nextPlace+new Vector3(0, 2, 0), currentDiretcion * rayReach,out hit,Obstacles))
            {
                Debug.Log(hit.transform.tag);
                
                if (hit.transform.tag == "Obstacle")
                {
                    playerMoved = false;

                }
                else
                {
                    playerMoved = true;
                }
                */
            if (playerMoved)
            {
                playerMoved = false;
                LeanTween.move(gameObject, nextPlace, 0.28f);
                animator.SetTrigger("hop");
                isHoping = true;
                moved = false;
                scriptRef.MoveTiles((int)direction);
            }
        }
    }

    //LeanTween.move(gameObject, nextPlace, 0.28f).setOnComplete(()=>TweenCallBack(2));

    public void TweenCallBack(int s)
    {

    }


public void FinishedHop()
    {
        isHoping = false;
    }
}