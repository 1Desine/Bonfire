using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bonfire : InteractableObject {
    public static Bonfire Instance { get; private set; }

    public event EventHandler OnOverflow;
    public event EventHandler OnConsumableAdded;

    public event EventHandler<OnDiedEventArgs> OnDied;
    public class OnDiedEventArgs : EventArgs {
        public float lifeTime;
    }

    private float healthMax = 30f;
    private float health;

    private float lifeTime;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("Bonfire - more then one Instance");
        }
        Instance = this;
        health = healthMax;
    }

    private void Update() {
        lifeTime += Time.deltaTime;

        health -= Time.deltaTime;
        OnConsumableAdded?.Invoke(this, EventArgs.Empty);

        if(health < 0) {
            OnDied?.Invoke(this, new OnDiedEventArgs {
                lifeTime = lifeTime
            });
        }
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
