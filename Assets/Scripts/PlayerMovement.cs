using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed;
    public float jumpSpeed;
    [SerializeField]
    private float jumpHorizontalSpeed;
    public float jumpButtonGracePeriod;

    [SerializeField]
    private Transform cameraTransform;
    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime; // float? means it's nullable
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    void Start(){
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude)/2;

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            inputMagnitude *= 2;
        }

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded){
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump")){
            jumpButtonPressedTime = Time.time;
        }

        // checks if the character controller has been grounded recently
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod){
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            // checks if the jump button has been pressed recently
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod){
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else{
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2){
                animator.SetBool("IsFalling", true);
            }
        }   

        // check if character is moving
        if (movementDirection != Vector3.zero){
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else{
            animator.SetBool("IsMoving", false);
        }

        if (isGrounded == false){
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    // using root motion
    private void OnAnimatorMove(){
        if(isGrounded){
            Vector3 velocity = animator.deltaPosition;
            velocity.y =  ySpeed * Time.deltaTime;
            characterController.Move(velocity);
        }
    }

    // private void OnApplicationFocus(bool focus){
    //     if(focus){
    //         Cursor.lockState = CursorLockMode.Locked;
    //     }
    //     else{
    //         Cursor.lockState = CursorLockMode.None;
    //     }
    // }
}
