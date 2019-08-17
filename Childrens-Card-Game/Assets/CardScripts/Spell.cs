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
        Debug.Log("HELLO");

        // When this card is played.
        for (int i = 0; i < e.AfterList.Count; i++)
        {

            Debug.Log("e.BeforeList[i].status: " +  e.BeforeList[i].status);
            Debug.Log("e.AfterList[i].status: " + e.AfterList[i].status);
            Debug.Log("e.AfterList: " + e.AfterList[i].name);
            Debug.Log("target: " + target.name);


            if (e.BeforeList[i].status != Status.BeingPlayed && e.AfterList[i].status == Status.BeingPlayed
                && e.AfterList[i] == this
                && target != null)
            {

                RunEffects(e.AfterList, BattleCry);

            }
        }
    }

    public void BattleCry(List<Card> AfterList)
    {

        Card target = AfterList[0];

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
