using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;

    public event EventHandler<OnButtonEventArgs> OnRun;
    public event EventHandler<OnButtonEventArgs> OnJump;
    public class OnButtonEventArgs : EventArgs {
        public bool started;
    }
    
  
    private void Awake() {
        playerInput = new PlayerInput();
        playerInput.PlayerLocomotion.Run.started += Run_Started;
        playerInput.PlayerLocomotion.Run.canceled += Run_Canceled;

        playerInput.PlayerLocomotion.Jump.started += Jump_Started;
        playerInput.PlayerLocomotion.Jump.canceled += Jump_canceled;

    }
    
    private void Jump_Started(InputAction.CallbackContext context) {
        OnJump?.Invoke(this, new OnButtonEventArgs { started = true });
    }
    
    private void Jump_canceled(InputAction.CallbackContext context) {
        OnJump?.Invoke(this, new OnButtonEventArgs { started = false });
    }

    private void Run_Started(InputAction.CallbackContext context)
    {
        OnRun?.Invoke(this, new OnButtonEventArgs() { started = true });
    }
    
    private void Run_Canceled(InputAction.CallbackContext context)
    {
        OnRun?.Invoke(this, new OnButtonEventArgs() { started = false });
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetCurrentMovementInput() {
        return playerInput.PlayerLocomotion.Move.ReadValue<Vector2>();
    }
    
    private void OnEnable() {
        playerInput.PlayerLocomotion.Enable();
    }

    private void OnDisable() {
        playerInput.PlayerLocomotion.Disable();
    }
}
