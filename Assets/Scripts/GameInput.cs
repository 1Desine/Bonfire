using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameInput : MonoBehaviour {
    public static GameInput Instance;

    public event EventHandler OnInteract;


    private PlayerInputActions playerInputActions;



    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Start() {
        playerInputActions.Player.Interact.performed += Interact_performed;

        Bonfire.Instance.OnDied += Bonfire_OnDied;
    }

    private void Bonfire_OnDied(object sender, Bonfire.OnDiedEventArgs e) {
        playerInputActions.Player.Disable();
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
