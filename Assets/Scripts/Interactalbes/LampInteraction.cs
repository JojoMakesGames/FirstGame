using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteraction : PossessableObject
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Possessed) {
            if(Input.GetButtonDown("Action"))
            {
                SpookyAction();
            }
            if(Input.GetButtonDown("Possess"))
            {
                Player.SetActive(true);
                Possessed = false;
            }
        }
    }

    public override void SpookyAction() {
        Debug.Log("Boo!");
    }
}
