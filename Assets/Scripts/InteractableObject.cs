using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {



    public virtual void Interact(GameObject player, Transform parentHoldPoint, ref ConsumableObject objectHolding) {
    }


}
