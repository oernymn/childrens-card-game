using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWithEffect : Card
{

    public int atk = 2;

    


    //public override int Atk { get; set; }

    public override void CardEffect(object sender, EffectEventArgs e)
    {
        
    }

    private void Start()
    {
        GetComponent<Card>().type = Type.Spell;



        int[] indexes = { 0, 1, 2 };
            Functions.TransferCard(this.transform, hand1, deck1, indexes);
            
        
    }

}
