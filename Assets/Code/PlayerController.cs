using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Dis be the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public bool startCanMove = true;
    public Transform startLocation;
    public UnityEvent<GameObject> onInteraction;
    public bool canMove { get; set; }

    private Camera lookCamera;
    private CharacterController characterController;
    private MeshRenderer capsuleMeshRenderer;

    private Transform followTransform;

    private float lookSensitivity = 2;
    private float moveSpeed = 4;
    private float gravity = -9;

    private float quitTimer = 0;

    private Vector2 lookAbsolute;
    private Vector2 lookSmooth;
    private Vector3 targetCharacterDirection;
    private Vector3 targetDirection;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        capsuleMeshRenderer = GetComponent<MeshRenderer>();
        lookCamera = GetComponentInChildren<Camera>();
        
        capsuleMeshRenderer.enabled = false;

        if (startLocation != null)
        {
            transform.position = startLocation.position;
            transform.rotation = startLocation.rotation;
        }

        canMove = startCanMove;

        targetDirection = lookCamera.transform.localRotation.eulerAngles;
        targetCharacterDirection = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            UpdateCameraLook();
        UpdateCharacterMove();
        UpdateCharacterGravity();
        UpdateInteraction();
        UpdateCursorLock();
    }

    private void UpdateCameraLook()
    {
        var targetLookOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);
        var lookDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        /*if (lookDelta == Vector2.zero)
        {
            lookDelta = new Vector2(Input.GetAxis("LookHorizontal"), Input.GetAxis("LookVertical"));
        }*/
        lookDelta = Vector2.Scale(lookDelta, new Vector2(lookSensitivity * 3, lookSensitivity * 3));
        lookSmooth.x = Mathf.Lerp(lookSmooth.x, lookDelta.x, 1f / 3);
        lookSmooth.y = Mathf.Lerp(lookSmooth.y, lookDelta.y, 1f / 3);
        lookAbsolute += lookSmooth;
        lookCamera.transform.localRotation = Quaternion.AngleAxis(-lookAbsolute.y, targetLookOrientation * Vector3.right);
        lookAbsolute.y = Mathf.Clamp(lookAbsolute.y, -90, 90);
        lookCamera.transform.localRotation *= targetLookOrientation;
        transform.localRotation = Quaternion.AngleAxis(lookAbsolute.x, transform.up);
        transform.localRotation *= targetCharacterOrientation;
    }

    private void UpdateCharacterGravity()
    {
        if (!characterController.isGrounded)
        {
            characterController.Move(transform.up * gravity * Time.deltaTime);
        }
    }

    private void UpdateCharacterMove()
    {
        if (!canMove)
        {
            //transform.position = followTransform.position - Vector3.up;
            return;
        }

        var moveInput = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime) * moveSpeed;
        var move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move);
    }

    private void UpdateCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Cursor.lockState == CursorLockMode.None && Input.anyKeyDown)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void UpdateInteraction()
    {
        if (canMove)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3") || Input.GetButtonDown("Jump"))
            {
                /*followTransform = interactionTrigger.inTrigger[0].transform;
                canMove = false;*/

                onInteraction.Invoke(gameObject);
            }
        }

        quitTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quitTimer > 0)
            {
                Application.Quit();
            }
            else
            {
                quitTimer = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            lookSensitivity = Mathf.Clamp(lookSensitivity - 0.1f, 0.1f, 10f);
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            lookSensitivity = Mathf.Clamp(lookSensitivity + 0.1f, 0.1f, 10f);
        }
    }

}
