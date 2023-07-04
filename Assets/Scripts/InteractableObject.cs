using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {


    [SerializeField] private string name;

    public virtual void Interact(GameObject player, Transform parentHoldPoint, ref ConsumableObject objectHolding) {
    }



    public string GetName() {
        return name;
    }
}
