using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ConsumableObject : InteractableObject {

    [SerializeField] private int energyValue;


    public void SetParent(Transform parent) {
        if(parent != null) {
            this.transform.SetParent(parent);
            this.transform.localPosition = Vector3.zero;
        } else {
            Vector3 position = transform.position;
            this.transform.SetParent(parent);
            this.transform.position = new Vector3(position.x, 0, position.z);
        }
    }

    public int GetEnergyValue() {
        return energyValue;
    }

    public override void Interact(GameObject player, Transform parentHoldPoint, ref ConsumableObject objectHolding) {
        objectHolding = this;
        SetParent(parentHoldPoint);
    }



}
