using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificCard : Card
{

    //public override int Atk { get; set; }

    public override void CardEffect(object sender, ListEventArgs e)
    {

        Debug.Log("Specific card effect was run!");
        Debug.Log(e.Data);

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

    private void Update()
    {
      //  Debug.Log(Atk);
    }


    
}
