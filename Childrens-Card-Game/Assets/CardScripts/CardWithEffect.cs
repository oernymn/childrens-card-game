using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWithEffect : Card
{

    public int atk;

    //public override int Atk { get; set; }

    public override void CardEffect(object sender, EffectEventArgs e)
    {
      
       

        if (e.before.GetComponent<Card>().parentName == "deck1" && e.after.transform.parent == hand1)
        {
             Debug.Log($"{transform.name} got transfered from {e.before.GetComponent<Card>().parentName} to {e.after.transform.parent.name}");
        }
        
        

    }

    private void Start()
    {

            int[] indexes = { 0, 1, 2 };
            Functions.TransferCard(this.transform, hand1, deck1, indexes);
            
        
    }

}
