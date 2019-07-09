using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{

    
    

    public GameObject deck1;
    public GameObject hand1;

         private void Awake()
    {
        deck1 = GameObject.Find("deck1");
        hand1 = GameObject.Find("hand1");
    }


    // Can't set the value here, have to do it in Unity
    public int atk = 3;

    public void cardEffect(Transform before, Transform after)
    {
        if (before.transform.parent.name == "deck1" && after.transform.parent.name == "hand1")
        {
          Debug.Log("SUCCESS! Pog");
        } else
        {
            Debug.Log("EPIC FAIL :tf:");
        }
    }


    
   
         
}






