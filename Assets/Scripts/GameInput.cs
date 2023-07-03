using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameInput : MonoBehaviour {
    public static GameInput Instance;

    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlt;


    private PlayerInputActions playerInputActions;



    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Start() {
        playerInputActions.Player.Interact.performed += Interact_performed;
        //playerInputActions.Player.InteractAlt.performed += InteractAlt_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {
        

    }


    public Vector2 GetMovementVectorNormalized() {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
    public Vector2 GetMouseVector() {
        return playerInputActions.Player.Look.ReadValue<Vector2>();
    }


}
