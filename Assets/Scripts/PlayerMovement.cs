using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float Speed = 15f;
    public float Gravity = -9.81f * 2;
    public float JumpHeight = 3f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    private Vector3 velocity;

    private bool isGrounded;

    private void Update()
    {
        Move(GetMoveInput("Horizontal"), GetMoveInput("Vertical"));
        Jump();

        
    }

    void Move(float x, float y)
    {
        Vector3 move = transform.right * x + transform.forward * y;

        characterController.Move(move * Speed * Time.deltaTime);
    }

    float GetMoveInput(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        velocity.y += Gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }


}
