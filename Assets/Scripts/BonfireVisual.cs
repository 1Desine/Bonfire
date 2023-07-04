using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireVisual : MonoBehaviour {

    [SerializeField] private Bonfire bonfire;
    [SerializeField] private GameObject lightObject;
    [SerializeField] private List<GameObject> woodList;

    private Light lighting;
    private float healthNormalized;

    private float lightingRangeMax;
    private float lightingIntensityMax;


    private void Awake() {
        lighting = lightObject.GetComponent<Light>();
        lightingRangeMax = lighting.range;
        lightingIntensityMax = lighting.intensity;
    }


    private void Update() {
        healthNormalized = bonfire.GetHealthNormalized();

        HandleAmountOfWood();
        HandleLightingEffect();
    }


    private void HandleAmountOfWood() {
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

    private void HandleLightingEffect() {
        lighting.range = lightingRangeMax * healthNormalized;
        lighting.intensity = lightingIntensityMax * healthNormalized + 100;
    }


}
