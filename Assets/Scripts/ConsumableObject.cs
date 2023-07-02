using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObject : InteractableObject {
    [SerializeField] private ConsumableObjectSO consumableObjectSO;





    public void SetParent(Transform parent) {
        this.transform.SetParent(parent);
        this.transform.position = parent.position;
        this.transform.localPosition = Vector3.zero;
    }

}
