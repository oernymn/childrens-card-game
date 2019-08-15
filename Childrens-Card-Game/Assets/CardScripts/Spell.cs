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
        if (e.before.status != Status.BeingPlayed && e.after.status == Status.BeingPlayed
            && e.after == this)
        {

            Card target = e.after.target;
            


            RunEffects(AfterList, BattleCry);

        }
    }

    public void BattleCry(List<Card> AfterList)
    {

        if (target.type == CardType.Minion)
        {

            Debug.Log($"From {target.stats.currentHealth} health");
            target.stats.currentHealth -= 2;
            Debug.Log($"To {target.stats.currentHealth} health");
        }
    }

    public override List<Card> GetTargets()
    {
        Transform enemyBoard1 = GetContainer(this, true, board1Index);
        Debug.Log("Enemy Board1: " + enemyBoard1.name + "Alegiance: " + enemyBoard1.parent.name);

        List<Card> selection = new List<Card>();

        foreach (Transform card in enemyBoard1)
        {
            Card CARD = card.GetComponent<Card>();
            Debug.Log("Card Name: " + card.name);

            selection.Add(CARD);

        }
        return selection;
    }

    private void Awake()
    {
        type = CardType.Spell;
    }
    
}
