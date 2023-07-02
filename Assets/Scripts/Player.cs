using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;


    public event EventHandler<OnSelectedObjectChengedEventArgs> OnSelectedObjectChenged;
    public class OnSelectedObjectChengedEventArgs : EventArgs {
        public InteractableObject selectedObject;
    }
    private InteractableObject selectedObject;


    private float moveSpeed = 10f;

    private bool isWalking;


    private void Awake() {
        if(Instance != null) {
            Debug.LogError("Player - more then one Instance");
        }
        Instance = this;
    }

    void Start() {

    }



    void Update() {
        HandleMovement();
        HandleInteractions();

        if(selectedObject == null) {
            Debug.Log("Player, selectedObject - null");
        }

    }



    private void HandleInteractions() {
        Vector2 moveInput = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);

        float interactDistance = 1f;

        float playerRadius = .5f;
        float playerHight = 1f;
        if(moveDir != Vector3.zero) {
            if(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, out RaycastHit raycastHit, interactDistance)) {
                if(raycastHit.transform.TryGetComponent(out InteractableObject interactableObject)) {
                    if(interactableObject != selectedObject) {
                        SetSelectedObject(interactableObject);
                    }
                } else {
                    SetSelectedObject(null);
                }
            } else {
                SetSelectedObject(null);
            }
        }

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
            transform.position += moveDir * moveDistance;
        }
        isWalking = canMove && moveDir != Vector3.zero;
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedObject(InteractableObject selectedObject) {
        this.selectedObject = selectedObject;

        OnSelectedObjectChenged?.Invoke(this, new OnSelectedObjectChengedEventArgs {
            selectedObject = selectedObject
        });
    }


}
