using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;

public class WheneverMinion : Card
{

    public override void WheneverCardEffect (object sender, EffectEventArgs e)
    {

        
        // When a card is played.
        if (e.before.status != Status.BeingPlayed && e.after.status == Status.BeingPlayed)
        {
     
            
        }
    }


    private void Start()
    {
        type = CardType.Minion;

    }

}
