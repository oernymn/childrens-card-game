using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificCard : Minion
{

    public override int Atk { get; set; }



    Status status = Status.Attacking;

    public override void CardEffect(Transform before, Transform after)
    {

        Debug.Log("Specific card effect was run!");

        if (before.transform.parent.name == "deck1" && after.transform.parent.name == "hand1")
        {
             Debug.Log("SUCCESS! Pog");
        }
        else
        {
            Debug.Log("EPIC FAIL :tf:");
        }

    }

    private void Update()
    {
        Debug.Log(Atk);
    }

}
