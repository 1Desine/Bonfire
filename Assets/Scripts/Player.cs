using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    private const string SELECTABLE_OBJECT_TAG = "SelectableObject";

    [SerializeField] private Transform objectHoldingPoint;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float mouseSensitivity = 0.1f;

    public event EventHandler<OnSelectedObjectChengedEventArgs> OnSelectedObjectChenged;
    public class OnSelectedObjectChengedEventArgs : EventArgs {
        public InteractableObject selectedObject;
    }
    private InteractableObject selectedObject;
    private ConsumableObject objectHolding;

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
        HandleRotation();
    }


    private void GameInput_OnInteract(object sender, EventArgs e) {
        if(selectedObject != null) {
            // Player is looking at something
            if(!IsHoldingSomething()) {
                // Player is not holding anithing
                if(selectedObject.TryGetComponent(out InteractableObject interactableObject)) {
                    interactableObject.Interact(this.gameObject, objectHoldingPoint, ref objectHolding);
                }
            } else {
                // Player is holding something
                if(selectedObject == Bonfire.Instance) {
                    Bonfire.Instance.BurnObject(objectHolding);
                }
            }
        } else {
            // Player is not looking at anithing
            if(objectHolding != null) {
                // Player holding something
                objectHolding.SetParent(null);
                objectHolding = null;
            }
        }
    }


    private void HandleInteractions() {
        float interactDistance = 1f;

        float playerRadius = .5f;
        float playerHight = 1f;
        if(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, transform.forward, out RaycastHit raycastHit, interactDistance)) {
            if(raycastHit.collider.TryGetComponent(out InteractableObject interactableObject)) {
                if(raycastHit.collider.gameObject != selectedObject) {
                    SetSelectedObject(interactableObject);
                }
            } else {
                SetSelectedObject(null);
            }
        } else {
            SetSelectedObject(null);
        }
    }
    private void HandleMovement() {
        Vector2 inputDir = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = transform.forward * inputDir.y + transform.right * inputDir.x;

        float rotationSpeed = 10f;
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .5f;
        float playerHight = 1f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDir, moveDistance);


        if(IsHoldingSomething()) {
            playerVisual.forward = transform.forward;
        } else {
            Vector2 mousePositionsDelta = GameInput.Instance.GetMouseVector();
            playerVisual.eulerAngles -= new Vector3(0, mousePositionsDelta.x * mouseSensitivity, 0);
            playerVisual.forward = Vector3.Slerp(playerVisual.forward, moveDir, rotationSpeed * Time.deltaTime);
        }

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
    private void HandleRotation() {
        Vector2 mousePositionsDelta = GameInput.Instance.GetMouseVector();
        transform.eulerAngles += new Vector3(0, mousePositionsDelta.x * mouseSensitivity, 0);
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

    private bool IsHoldingSomething() {
        return objectHolding != null;
    }

}
