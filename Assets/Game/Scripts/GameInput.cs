using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }
    public event EventHandler OnMouseClicked;
    public event EventHandler OnPlayerJump;
    public event EventHandler OnGamePaused;
    public event EventHandler OnPlayerChangeEnvironment;
    private GameInputActions inputActions;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
        inputActions = new GameInputActions();
    }

    private void Start() {
        inputActions.Enable();
        inputActions.Player.Jump.performed += OnPlayerJumpPerformed;
        inputActions.Player.EnvironmentChange.performed += OnPlayerChangeEnvironmentPerformed;
        inputActions.Player.PauseGame.performed += OnGamePausedPerformed;
        inputActions.Player.ExitCredits.performed += OnMouseClickedPerformed;
    }

    private void OnDestroy() {
        inputActions.Player.Jump.performed -= OnPlayerJumpPerformed;
        inputActions.Player.EnvironmentChange.performed -= OnPlayerChangeEnvironmentPerformed;
        inputActions.Player.PauseGame.performed -= OnGamePausedPerformed;
        inputActions.Player.ExitCredits.performed -= OnMouseClickedPerformed;
        inputActions.Dispose();
    }

    
    private void OnMouseClickedPerformed(InputAction.CallbackContext callbackContext) {
        OnMouseClicked?.Invoke(this, EventArgs.Empty);
    }


    private void OnPlayerJumpPerformed(InputAction.CallbackContext callbackContext) {
        OnPlayerJump.Invoke(this, EventArgs.Empty);
    }

    private void OnGamePausedPerformed(InputAction.CallbackContext callbackContext) {
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    private void OnPlayerChangeEnvironmentPerformed(InputAction.CallbackContext callbackContext) {
        OnPlayerChangeEnvironment.Invoke(this, EventArgs.Empty);
    }

    public float GetHorizontalMovement() {
        float input = inputActions.Player.Movement.ReadValue<float>();
        return input;
    }

    public float GetVerticalMovement() {
        float input = inputActions.Player.Climb.ReadValue<float>();
        return input;
    }
}
