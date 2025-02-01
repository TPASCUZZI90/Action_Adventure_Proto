using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpForce = 5f;

    private float verticalVelocity = 0f;
    private float initMoveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initMoveSpeed = moveSpeed;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {

        float horizontalInput = Input.GetAxis("HorizontalL");
        float verticalInput = Input.GetAxis("VerticalL");

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        Vector3 forward = (characterController.isGrounded) ? cameraTransform.forward : transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 moveDirection = (right * horizontalInput + forward * verticalInput).normalized;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Jump();

        moveSpeed = (characterController.isGrounded) ? initMoveSpeed : initMoveSpeed / 2f;

        Vector3 finalMove = moveDirection * moveSpeed + Vector3.up * verticalVelocity;
        characterController.Move(finalMove * Time.deltaTime);

    }

    void Jump()
    {
        
        if(characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
    }

    public void AlignWithCamera()
    {
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = targetRotation;
    }
}
