using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private GameObject survivedForObject;

    [SerializeField] private List<GameObject> gameObjectUIList;

    private TextMeshProUGUI timeSurvivedText;

    private void Awake() {
        timeSurvivedText = survivedForObject.GetComponent<TextMeshProUGUI>();

        foreach(GameObject gameObjectUI in gameObjectUIList) {
        gameObjectUI.SetActive(false);
        }
    }

    private void Start() {
        Bonfire.Instance.OnDied += Bonfire_OnDied;
    }

    private void Bonfire_OnDied(object sender, Bonfire.OnDiedEventArgs e) {
        foreach(GameObject gameObjectUI in gameObjectUIList) {
            gameObjectUI.SetActive(true);
        }

        timeSurvivedText.text += e.lifeTime.ToString("#,##");

        Bonfire.Instance.OnDied -= Bonfire_OnDied;
    }
}
