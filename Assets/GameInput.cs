using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;

    public event EventHandler OnRun;
    public event EventHandler OnJump;
    
  
    private void Awake() {
        playerInput = new PlayerInput();
        playerInput.PlayerLocomotion.Run.performed += OnRunPerformed;
        
     }

    private void OnRunPerformed(InputAction.CallbackContext obj)
    {
        OnRun?.Invoke(this, EventArgs.Empty);
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
