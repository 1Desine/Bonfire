using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;



    private void Start() {
        Player.Instance.OnGrabMashroom += Player_OnGrabMashroom;
        Player.Instance.OnDropMashroom += Player_OnDropMashroom;
        Player.Instance.OnGrabLog += Player_OnGrabLog;
        Player.Instance.OnDropLog += Player_OnDropLog;
        Player.Instance.OnBurnObject += Player_OnBurnObject;
    }

    private void Player_OnBurnObject(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.burnObject, Bonfire.Instance.transform.position);
    }
    private void Player_OnGrabLog(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.grabLog, Camera.main.transform.position);
    }
    private void Player_OnDropLog(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.dropLog, Camera.main.transform.position);
    }
    private void Player_OnGrabMashroom(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.grabMushroom, Camera.main.transform.position);
    }
    private void Player_OnDropMashroom(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.dropMushroom, Camera.main.transform.position);
    }




    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = .2f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = .2f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }



}
