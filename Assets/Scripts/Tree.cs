using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : InteractableObject {


    [SerializeField] private GameObject ObjectToSpawn;


    public override void Interact(GameObject player, Transform parentHoldPoint, ref ConsumableObject objectHolding) {
        GameObject nextObject = Instantiate(ObjectToSpawn);
        nextObject.transform.position = this.transform.position + player.transform.forward;
        nextObject.transform.LookAt(Player.Instance.transform);
        nextObject.transform.SetParent(null);

        Destroy(this.gameObject);
    }



}
