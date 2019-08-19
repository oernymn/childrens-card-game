using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;
using static GetSet;
using static CardEffect;


public class Spell : Card
{
    public override void AfterCardEffect(object sender, EffectEventArgs e)
    {

        // When this card is played.
        for (int i = 0; i < e.AfterList.Count; i++)
        {

       //     Debug.Log($"e.BeforeList[{i}].status: {e.BeforeList[i].status} | e.AfterList[{i}].status: {e.AfterList[i].status} | e.AfterList[{i}]: {e.AfterList[i].name}");

            if (e.BeforeList[i].status != Status.BeingPlayed && e.AfterList[i].status == Status.BeingPlayed
                && e.AfterList[i] == this
                && target != null)
            {
                RunEffects(new List<Card> { target }, BattleCry);
            }
        }
    }

    public void BattleCry(List<Card> AffectedList)
    {
        foreach (Card card in AffectedList)
        {
            if (card.type == CardType.Minion && card.stats != null)
            {

                Debug.Log($"From {card.stats.currentHealth} health");
                card.stats.currentHealth -= 2;
                Debug.Log($"To {card.stats.currentHealth} health");
            }
        }
    }

    public override List<Card> GetTargets()
    {
        Transform enemyBoard1 = GetContainer(this, true, board1Index);
        List<Card> selection = new List<Card>();

        foreach (Transform card in enemyBoard1)
        { 

            selection.Add(card.GetComponent<Card>());

        }
        return selection;
    }

    private void Start()
    {
        type = CardType.Spell;
    }

}
