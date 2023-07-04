using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BonfireVisual : MonoBehaviour {

    [SerializeField] private Bonfire bonfire;
    [SerializeField] private GameObject lightObject;
    [SerializeField] private List<GameObject> woodList;

    private Light lighting;
    private float healthNormalized;

    private float lightingRangeMax;
    private float lightingIntensityMax;


    private float intecityTimer;
    private float intecityTimerMax = 2f;

    private void Awake() {
        lighting = lightObject.GetComponent<Light>();
        lightingRangeMax = lighting.range;
        lightingIntensityMax = lighting.intensity;
    }


    private void Update() {
        healthNormalized = bonfire.GetHealthNormalized();
        intecityTimer -= Time.deltaTime;

        HandleAmountOfWood();
        HandleLightingEffect();

        bonfire.OnOverflow += Bonfire_OnOverflow;
    }

    private void Bonfire_OnOverflow(object sender, System.EventArgs e) {
        intecityTimer = intecityTimerMax;
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
        float rangeModefier = 5f;

        lighting.range = lightingRangeMax * healthNormalized + rangeModefier;
        if(intecityTimer > 0) {
            float lightingIntensityMax_intence = 2f;
            lighting.intensity = math.lerp(lighting.intensity, lightingIntensityMax * lightingIntensityMax_intence, 3f * Time.deltaTime);
        } else {
            lighting.intensity = math.lerp(lighting.intensity, lightingIntensityMax * healthNormalized, 1f * Time.deltaTime);
        }
    }


}
