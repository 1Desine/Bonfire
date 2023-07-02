using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireVisual : MonoBehaviour {
    [SerializeField] private GameObject bonfire;
    [SerializeField] private List<GameObject> VisualSelectedList;


    private void Start() {
        Player.Instance.OnSelectedObjectChenged += Player_OnSelectedObjectChenged;
    }

    private void Player_OnSelectedObjectChenged(object sender, Player.OnSelectedObjectChengedEventArgs e) {
        if(e.selectedObject == null) {
            Debug.Log("BonfireVisual, e.selectedObject - null");
        }

        if(e.selectedObject != null) {
            if(e.selectedObject.interactableObject == bonfire) {
                Show();
            } else {
                Hide();
            }
        } else {
            Hide();
        }
    }


    private void Show() {
        foreach(GameObject visual in VisualSelectedList) { 
        visual.SetActive(true);
        }
    }
    private void Hide() {
        foreach(GameObject visual in VisualSelectedList) {
            visual.SetActive(false);
        }
    }

}
