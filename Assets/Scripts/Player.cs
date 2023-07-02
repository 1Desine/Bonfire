using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;

    private const string SELECTABLE_OBJECT_TAG = "SelectableObject";

    [SerializeField] private Transform objectHoldingPoint;

    public event EventHandler<OnSelectedObjectChengedEventArgs> OnSelectedObjectChenged;
    public class OnSelectedObjectChengedEventArgs : EventArgs {
        public InteractableObject selectedObject;
    }
    private InteractableObject selectedObject;
    private ConsumableObject objectHolding;

    private float moveSpeed = 10f;
    private bool isWalking;


    private void Awake() {
        if(Instance != null) {
            Debug.LogError("Player - more then one Instance");
        }
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnInteract += GameInput_OnInteract;
    }


    void Update() {
        HandleMovement();
        HandleInteractions();

    }


    private void GameInput_OnInteract(object sender, System.EventArgs e) {
        if(selectedObject != null) {
            selectedObject.Interact();
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
                if(raycastHit.collider.TryGetComponent(out InteractableObject selectableObject)) {
                    if(raycastHit.collider.gameObject != selectedObject) {
                        SetSelectedObject(selectableObject);
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
