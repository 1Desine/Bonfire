using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance;


    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
    }


    private void Update() {
        playerInputActions.Player.Move.ReadValue<Vector2>();
    }





}
