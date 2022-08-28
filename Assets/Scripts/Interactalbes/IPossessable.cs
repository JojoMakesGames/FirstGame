using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PossessableObject : MonoBehaviour
{
    public bool CanMove {get; set;}
    public bool CanPossess {get; set;}
    protected Vector3 PlayerLocation {get; set;}
    public abstract void SpookyAction();

    protected void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            Debug.Log("Entering Area");
            CanPossess = true;
        }
    }

    protected void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            Debug.Log("Exiting Area");
            CanPossess = false;
        }
    }
}
