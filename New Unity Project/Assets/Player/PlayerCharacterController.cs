using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    private float gravity = -50f;
    private CharacterController characterController;
    private Vector3 velocity;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
    }
}
