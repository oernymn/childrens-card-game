using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;


public class Spell : Card
{
   


    public override void AfterCardEffect (object sender, EffectEventArgs e)
    {
        // When this card is played.
        if (e.before.GetComponent<Card>().status != Status.BeingPlayed && e.after.GetComponent<Card>().status == Status.BeingPlayed
            && e.after == transform)
        {

            Card target = e.after.target;
            
            RunEffects(target, this, BattleCry);

        }
    }

    public void BattleCry(Card target, Card affecter)
    {

        if (target.type == CardType.Minion)
        {

            Debug.Log($"From {target.GetComponent<Stats>().currentHealth} health");
            target.GetComponent<Stats>().currentHealth -= 2;
            Debug.Log($"To {target.GetComponent<Stats>().currentHealth} health");
        }
    }

    public override List<Card> GetTargets()
    {
        Transform enemyBoard1 = GetContainer(this, true, board1Index);

        List<Card> selection = new List<Card>();

        foreach (Card card in enemyBoard1)
        {
            selection.Add(card);

        }
        return selection;
    }

    private void Awake()
    {
        type = CardType.Spell;
    }
    
}
