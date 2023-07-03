using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonfireUI : MonoBehaviour {

    [SerializeField] private Bonfire bonfre;
    [SerializeField] private GameObject bar;

    float bonfireHealth;

    private Image healthBarImage;

    private void Start() {
        healthBarImage = bar.GetComponent<Image>();
    }


    private void Update() {
        bonfireHealth = bonfre.GetHealthNormalized();
        healthBarImage.fillAmount = bonfireHealth;

        healthBarImage.color = new Color(1 - bonfireHealth, bonfireHealth, 0, 1.0f);
    }



}
