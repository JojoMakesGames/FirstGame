using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossession : MonoBehaviour
{
    [HideInInspector]
    public PossessableObject Possessable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        possessObject();
    }

    void possessObject() 
    {
        if(Input.GetButtonDown("Possess") && Possessable is not null) {
            Possessable.PlayerLocation = gameObject.transform;
            Possessable.Player = gameObject;
            Possessable.Possessed = true;
            gameObject.SetActive(false);        
        }
    }
}
