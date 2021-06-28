using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float     runSpeed = 8f;

    private float               gravity = -50f;
    private CharacterController characterController;
    private Vector3             velocity;
    private bool                isGrounded;   
    private float               horizontalInput;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalInput = 1;

        //gledaj napred gde trcis
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        //proverava da li fikciona sfera koja je na nogama igraca dodiruje ground  //transform je na samom dnu igraca inace ne bi radilo!!!

        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);
       

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Debug.Log(isGrounded);

        characterController.Move(new Vector3(horizontalInput* runSpeed, 0, 0) * Time.deltaTime); 

        //add gravity
        velocity.y += gravity * Time.deltaTime;

        //Vertical Velocity
        characterController.Move(velocity * Time.deltaTime);
    }
}
