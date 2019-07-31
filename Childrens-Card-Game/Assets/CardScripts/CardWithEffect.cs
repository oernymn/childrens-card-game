using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWithEffect : Card
{




    

    public override void CardEffect(object sender, EffectEventArgs e)
    {
        // When this card is played.
        if (e.before.GetComponent<Card>().status != Status.BeingPlayed && e.after.GetComponent<Card>().status == Status.BeingPlayed
            && e.after == transform)
        {
            Card target = e.after.GetComponent<Card>().target.GetComponent<Card>();


            if (target.type == Type.Minion)
            {
                

                Debug.Log(target.health);

                target.health -= 2;
                Debug.Log(target.health);
            }
        }


        
        
        
    }

    public override List<Transform> GetSelection()
    {
        Transform enemyAllegiance = Functions.GetEnemyAllegiance(transform.parent.parent);

        Transform enemyBoard1 = enemyAllegiance.GetChild(board1Index);

        List<Transform> selection = new List<Transform>();

        foreach (Transform card in enemyBoard1)
        {
            selection.Add(card);
            

        }



        return selection;

    }

    private void Start()
    {
        GetComponent<Card>().type = Type.Spell;



        int[] indexes = { 0, 1, 2 };
            Functions.TransferCard(transform, transform.parent.parent.GetChild(Functions.handIndex), transform.parent.parent.GetChild(Functions.deckIndex), indexes);
            
       
    }

}
