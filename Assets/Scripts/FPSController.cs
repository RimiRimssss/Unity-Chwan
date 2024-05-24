using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float walkingChara = 5f;
    public float jumpChara = 8f;
    public float groundCheckDistance = 0.2f;
    public float lookSensitivityX = 3f;
    public float lookSensitivityY = 3f;
    public float MinYLookAngle = -90f;
    public float MaxYLookAngle = 90f;
    public Transform PlayerCamera;
    public float Gravity = -9.8f;
    public Vector3 velocity;
    private CharacterController characterController;
    private Animator animator;
    private bool isWalking;
    private bool isJumping;

    private float verticalRotation = 0f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        moveDirection.Normalize();

        float speed = walkingChara;

        characterController.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            velocity.y = jumpChara;
            animator.SetBool("isJumping", true);
        }
        else
        {
            velocity.y += Gravity * Time.deltaTime;
            animator.SetBool("isJumping", false);
        }

        characterController.Move(velocity * Time.deltaTime);

        if (PlayerCamera != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivityY;

            CameraLook(mouseX, mouseY);
        }

        if (verticalMovement != 0 || horizontalMovement != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void CameraLook(float mouseX, float mouseY)
    {
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, MinYLookAngle, MaxYLookAngle);

        PlayerCamera.localEulerAngles = new Vector3(verticalRotation, 0, 0);

        transform.Rotate(Vector3.up, mouseX);
    }
    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            return true;
        }
        return false;
    }
}