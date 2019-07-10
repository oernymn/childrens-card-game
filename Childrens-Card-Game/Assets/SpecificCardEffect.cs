﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificCardEffect : CardEffect
{
   public string message = "I'm here!";

    

    

   public override void cardEffect(Transform before, Transform after)
    {

        Debug.Log("Specific card effect was run!");

        if (before.transform.parent.name == "deck1" && after.transform.parent.name == "hand1")
        {
           // Debug.Log("SUCCESS! Pog");
        }
        else
        {
            Debug.Log("EPIC FAIL :tf:");
        }

    }



    public SpecificCardEffect()
    {

      
    }
}
