using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private GameInput gameInput;
    //private PlayerInput playerInput;
    private CharacterController characterController;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;

    private Vector3 currentRunMovement;
    private bool isRunPressed;
    
    float rotationFactorPerFrame = 15.0f;
    
    private void Awake() {
        //playerInput = new PlayerInput(); 
        //playerInput.PlayerLocomotion.Move.started += OnMove;
        //playerInput.PlayerLocomotion.Move.performed += OnMove;
        //playerInput.PlayerLocomotion.Move.canceled += OnMove;

        //playerInput.PlayerLocomotion.Run.started += OnRun;
        //playerInput.PlayerLocomotion.Run.canceled += OnRun;

        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        gameInput.OnRun += GameInput_OnRun;
        gameInput.OnJump += GameInput_OnJump;
    }

    private void GameInput_OnJump(object sender, EventArgs e) {
        throw new NotImplementedException();
    }

    private void GameInput_OnRun(object sender, GameInput.OnRunEventArgs e)
    {
        //isRunPressed = context.ReadValueAsButton();
        isRunPressed = e.started;
    }

    private void Update() {
        HandleMovement();
        HandleRotation();
        HandleGravity();
    }

    


    // can also make this function an inline lambda but this is better because we call this three different times
    // private void OnMove(InputAction.CallbackContext context) {
    //     currentMovementInput = context.ReadValue<Vector2>();
    //     currentMovement.x = currentMovementInput.x;
    //     currentMovement.z = currentMovementInput.y; // swizzle the values here
    //     isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    //     
    //     currentRunMovement.x = currentMovementInput.x * 3.0f;
    //     currentRunMovement.z = currentMovementInput.y * 3.0f;
    //     
    // }

    // Update is called once per frame
  

    private void HandleMovement() {
        currentMovementInput = gameInput.GetCurrentMovementInput();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y; // swizzle the values here
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        
        currentRunMovement.x = currentMovementInput.x * 3.0f;
        currentRunMovement.z = currentMovementInput.y * 3.0f;
        
        
        if (isRunPressed) {
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else {
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }
    
    private void HandleRotation() {
        Vector3 positionToLookAt;
        
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = currentMovement.z;
        
        Quaternion currentRotation = transform.rotation;
        
        // It'll create a quaternion with the location based on where the player is currently looking
        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        
        // We will slerp the location. slerp = spherical interpolation
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
    }

    private void HandleGravity() {
        // we need to set gravity like so because the character controller wont check for collisions unless some velocity is present
        
        if (characterController.isGrounded) {
            float groundedGravity = -0.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else {
            float gravity = -9.8f;
            currentMovement.y = gravity;
            currentRunMovement.y = gravity;
        }
    }


    public bool IsPlayerMoving() {
        return isMovementPressed;
    }

    public bool IsPlayerRunning() {
        return isRunPressed;
    }
    
}
