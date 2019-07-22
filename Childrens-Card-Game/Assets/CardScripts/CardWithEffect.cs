using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWithEffect : Card
{

    

    //public override int Atk { get; set; }

    public override void CardEffect(object sender, EffectEventArgs e)
    {

        if (this.transform.parent == null)
        {
            return;
        }

       // Debug.Log("Specific card effect was run!");

        if (e.before.GetComponent<Card>().parentName == "deck1" && e.after.transform.parent.name == "hand1")
        {
             Debug.Log("SUCCESS! Pog");
        }
        else
        {
            Debug.Log("EPIC FAIL :tf:");
        }
        

    }

    private void Start()
    {

            int[] indexes = { 0, 1, 2 };
            Functions.TransferCard(this.transform, hand1, deck1, indexes);
            
        
    }

}
