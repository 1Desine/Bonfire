using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class ConsumableObjectSO : ScriptableObject {

    [SerializeField] private GameObject scriptableObject;
    [SerializeField] private string name;

}
