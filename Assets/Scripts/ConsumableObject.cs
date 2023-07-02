using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObject : SelectableObject {
    [SerializeField] private ConsumableObjectSO consumableObjectSO;

    //private consumableObjectParent 


    public void SetParent(Transform parent) {
        this.SetParent(parent);
        this.transform.position = parent.position;
        this.transform.localPosition = Vector3.zero;
    }

}
