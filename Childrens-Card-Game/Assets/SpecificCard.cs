using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificCard : Card
{

    //public override int Atk { get; set; }

    public override void CardEffect(object sender, EventArgs e)
    {

        Debug.Log("Specific card effect was run!");
        Debug.Log(sender);

        /*
        if (before.transform.parent.name == "deck1" && after.transform.parent.name == "hand1")
        {
             Debug.Log("SUCCESS! Pog");
        }
        else
        {
            Debug.Log("EPIC FAIL :tf:");
        }
        */

    }

}
