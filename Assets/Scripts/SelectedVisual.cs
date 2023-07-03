using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SelectedVisual : MonoBehaviour {
    [SerializeField] private InteractableObject selectableObject;
    [SerializeField] private List<GameObject> VisualSelectedList;


    private void Start() {
        Player.Instance.OnSelectedObjectChenged += Player_OnSelectedObjectChenged;
    }

    private void Player_OnSelectedObjectChenged(object sender, Player.OnSelectedObjectChengedEventArgs e) {
        if(e.selectedObject == selectableObject) {
            Show();
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

    private void OnDestroy() {
        Player.Instance.OnSelectedObjectChenged -= Player_OnSelectedObjectChenged;
    }

}
