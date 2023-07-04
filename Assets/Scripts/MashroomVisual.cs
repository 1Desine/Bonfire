using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class MashroomVisual : MonoBehaviour {

    [SerializeField] private Material mashroomMaterial;


    private Vector3 colorVector = Vector3.forward;


    private void FixedUpdate() {
        colorVector = Quaternion.AngleAxis(Time.time % 6.28f, Vector3.one) * colorVector;


        mashroomMaterial.color = new Color(colorVector.x, colorVector.y, colorVector.z, 1f);
    }

}
