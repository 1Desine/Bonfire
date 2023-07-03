using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireParticles : MonoBehaviour {

    [SerializeField] private Bonfire bonfire;
    [SerializeField] private GameObject particles;

    private ParticleSystem particleSystem;

    [SerializeField] private float intenceEffectsTimerMax = 2f;
    private float intenceEffectsTimer;
    private float bonfireHelthNormalized;

    private float highestPointY;


    private void Awake() {
        particleSystem = particles.GetComponent<ParticleSystem>();
        highestPointY = particles.transform.position.y;
    }


    private void Start() {
        bonfire.OnOverflow += Bonfire_OnOverflow;
        bonfire.OnConsumableAdded += Bonfire_OnConsumableAdded;
    }

    private void Bonfire_OnConsumableAdded(object sender, System.EventArgs e) {

    }

    private void Bonfire_OnOverflow(object sender, System.EventArgs e) {
        intenceEffectsTimer = intenceEffectsTimerMax;
    }

    private void Update() {
        intenceEffectsTimer -= Time.deltaTime;
        SetIntenceEffects(intenceEffectsTimer > 0);

        bonfireHelthNormalized = Bonfire.Instance.GetHealthNormalized();
        Debug.Log(highestPointY * bonfireHelthNormalized);

        particles.transform.position = new Vector3(0, highestPointY * bonfireHelthNormalized, 0);
    }


    private void SetIntenceEffects(bool intence) {
        if(intence) {
            particleSystem.gravityModifier = -1f;
            particleSystem.emissionRate = 250;
        } else {
            particleSystem.gravityModifier = -0.4f;
            particleSystem.emissionRate = 100 * bonfireHelthNormalized;
        }
    }


}
