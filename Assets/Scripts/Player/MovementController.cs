using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float rotationSpeed = 10f; 

    private Rigidbody rb;
    private Animator anim;

    private Vector3 inputMovement; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInput();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacterToMoveDirection();
    }

    private void GetInput()
    {
        
        inputMovement.x = Input.GetAxis("Vertical");   

        
        inputMovement.z = -Input.GetAxis("Horizontal");

        
        inputMovement.y = 0; 

        
        if (inputMovement.magnitude > 1f)
        {
            inputMovement.Normalize();
        }
    }

    private void MoveCharacter()
    {
        
        Vector3 moveVelocity = inputMovement * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveVelocity);
    }

    private void RotateCharacterToMoveDirection()
    {
        
        if (inputMovement != Vector3.zero)
        {
           
            Vector3 targetDirection = inputMovement;
            targetDirection.y = 0; 

           
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

           
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateAnimations()
    {
        
        if (inputMovement.magnitude > 0.01f) 
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}