using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static cardEffectFunctions;

public class WheneverMinion : Card
{

    public override void CardEffect (object sender, EffectEventArgs e)
    {
        // When a card is played.
        if (e.before.GetComponent<Card>().status != Status.BeingPlayed && e.after.GetComponent<Card>().status == Status.BeingPlayed)
        {

            e.after = Instantiate(e.before, e.before.GetComponent<Card>().Parent);
            Debug.Log("e.after.name: " + e.after.name);


           
        }
    }



    private void Awake()
    {

        Functions.runEffects -= CardEffect;
        Functions.runWheneverEffects += CardEffect;
    }

    private void Start()
    {
        GetComponent<Card>().type = CardType.Spell;

        int[] indexes = { 0, 1, 2 };
        Functions.TransferCard(transform, transform.parent.parent.GetChild(Functions.handIndex), transform.parent.parent.GetChild(Functions.deckIndex), indexes);
            
       
    }

}
