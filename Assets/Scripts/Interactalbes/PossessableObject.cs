using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PossessableObject : MonoBehaviour
{
    public bool CanMove {get; set;}
    public bool CanPossess {get; set;}
    public bool Possessed {get; set;}
    [HideInInspector]
    public Transform PlayerLocation {get; set;}
    [HideInInspector]
    public GameObject Player {get; set;}
    public abstract void SpookyAction();

    protected void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            GameObject player = other.gameObject;
            CanPossess = true;
            PlayerPossession playerPossesion = player.GetComponent<PlayerPossession>();
            playerPossesion.Possessable = this;
        }
    }

    protected void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            GameObject player = other.gameObject;
            CanPossess = false;
            PlayerPossession playerPossesion = player.GetComponent<PlayerPossession>();
            playerPossesion.Possessable = null;
        }
    }
}
