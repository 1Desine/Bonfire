using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class CameraFollow: MonoBehaviour {

    [SerializeField] private Transform followObject;

    [SerializeField] private float followSpeed = 10f;


    private void Update() {
        transform.position = Vector3.Slerp(transform.position, followObject.position, followSpeed * Time.deltaTime);
    }


}
