using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    // Camera bobbing variables
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    private float defaultPosY = 0;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    // Additional variables for fading effect
    private Vector3 cameraVelocity = Vector3.zero;
    public float cameraFadeSpeed = 5f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the default camera position
        defaultPosY = 1.0f;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #region Handles Camera Bobbing
        if (Mathf.Abs(curSpeedX) > 0.1f || Mathf.Abs(curSpeedY) > 0.1f)
        {
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

            Vector3 cameraPos = playerCamera.transform.localPosition;
            cameraPos.y = Mathf.Lerp(cameraPos.y, defaultPosY + bobbingOffset, Time.deltaTime * cameraFadeSpeed);
            playerCamera.transform.localPosition = cameraPos;
        }
        else
        {
            Vector3 cameraPos = playerCamera.transform.localPosition;
            cameraPos.y = Mathf.Lerp(cameraPos.y, defaultPosY, Time.deltaTime * cameraFadeSpeed);
            playerCamera.transform.localPosition = cameraPos;
        }
        #endregion

        Application.targetFrameRate = 60;
    }
}
