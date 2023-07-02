using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;


    public event EventHandler<OnSelectedObjectChengedEventArgs> OnSelectedObjectChenged;
    public class OnSelectedObjectChengedEventArgs : EventArgs {
        public GameObject selectedObject;
    }


    private GameObject selectedObject;


    private float moveSpeed = 10f;

    private bool isWalking;


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
        float moveDistance = moveSpeed * Time.deltaTime;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);


        float playerRadius = .5f;
        float playerHight = 1f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, moveDistance);


        if(!canMove) {
            // Try only X

            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirX, moveDistance);

            if(canMove) {
                moveDir = moveDirX;
            } else {
                // Try only Z

                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirZ, moveDistance);

                if(canMove) {
                    moveDir = moveDirZ;
                }

            }
        }
        if(canMove) {
            isWalking = moveDir != Vector3.zero;
            transform.position += moveDir * moveDistance;
        }

        OnSelectedObjectChenged?.Invoke(this, new OnSelectedObjectChengedEventArgs {
            selectedObject = selectedObject
        });
    }


    public bool IsWalking() {
        return isWalking;
    }
}
