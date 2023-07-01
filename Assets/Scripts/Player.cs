using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;





    private float moveSpeed = 10f;


    private void Awake() {
        Instance = this;
    }

    void Start() {

    }



    void Update() {
        HandleMovement();
    }





    private void HandleMovement() {
        Vector2 moveInput = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;


        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }



}
