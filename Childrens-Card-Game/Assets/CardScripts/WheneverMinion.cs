using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static cardEffectFunctions;

public class WheneverMinion : Card
{

    public override void WheneverCardEffect (object sender, EffectEventArgs e)
    {

        
        // When a card is played.
        if (e.before.GetComponent<Card>().status != Status.BeingPlayed && e.after.GetComponent<Card>().status == Status.BeingPlayed)
        {
     

        }
    }


    private void Start()
    {
        GetComponent<Card>().type = CardType.Minion;

        
            
       
    }

}
