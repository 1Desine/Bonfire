using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireVisual : MonoBehaviour {

    [SerializeField] private Bonfire bonfire;
    [SerializeField] private List<GameObject> woodList;

    private float healthNormalized;



    private void Update() {
        HandleAmountOfWood();
    }


    private void HandleAmountOfWood() {
        healthNormalized = bonfire.GetHealthNormalized();
        float offset = .2f;

        for(int i = 0; i < woodList.Count; i++) {
            float occupancy = 1f / woodList.Count * i; // Normalized amount of objects
            if(healthNormalized + offset > 1 - occupancy) {
                woodList[i].gameObject.SetActive(true);
            } else {
                woodList[i].gameObject.SetActive(false);
            }
        }
    }

}
