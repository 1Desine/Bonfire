using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireVisual : MonoBehaviour {
    [SerializeField] private Bonfire bonfire;



    private void Start() {
        Player.Instance.OnSelectedObjectChenged += Player_OnSelectedObjectChenged;
    }

    private void Player_OnSelectedObjectChenged(object sender, Player.OnSelectedObjectChengedEventArgs e) {
        if(e.selectedObject == bonfire) {
            Show();
        } else {
            Hide();
        }
    }


    private void Show() {
        this.gameObject.SetActive(true);
    }
    private void Hide() {
        this.gameObject.SetActive(false);
    }

}
