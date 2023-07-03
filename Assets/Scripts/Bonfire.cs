using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bonfire : InteractableObject {
    public static Bonfire Instance { get; private set; }

    public event EventHandler OnOverflow;
    public event EventHandler OnConsumableAdded;

    private float healthMax = 30f;
    private float health;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("Bonfire - more then one Instance");
        }
        Instance = this;
        health = healthMax;
    }

    private void Update() {
        health -= Time.deltaTime;
        OnConsumableAdded?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized() {
        return health / healthMax;
    }



    public void BurnObject(ConsumableObject consumableObject) {
        health += consumableObject.GetEnergyValue();
        if (health > healthMax) {
            health = healthMax;
            OnOverflow?.Invoke(this, new EventArgs());
        }
        Destroy(consumableObject.gameObject);
    }

}
