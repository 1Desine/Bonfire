using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : InteractableObject {

    [SerializeField] private float healthMax = 60f;
    private float health;

    private void Awake() {
        health = healthMax;
    }

    private void Update() {
        health -= Time.deltaTime;
    }

    public float GetHealthNormalized() {
        return health / healthMax;
    }


}
