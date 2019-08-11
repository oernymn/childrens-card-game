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

            Card target = e.after.GetComponent<Card>().target;
            
            RunEffects(target, this, BattleCry);

        }
    }

    public void BattleCry(Card target, Card affecter)
    {

        if (target.type == CardType.Minion)
        {

            Debug.Log($"From {target.currentHealth} health");
            target.currentHealth -= 2;
            Debug.Log($"To {target.currentHealth} health");
        }
    }

    public override List<Transform> GetTargets()
    {
       Transform enemyBoard1 = GetEnemyContainer(board1);

        List<Transform> selection = new List<Transform>();

        foreach (Transform card in enemyBoard1)
        {
            selection.Add(card);

        }
        return selection;
    }

    private void Awake()
    {
        GetComponent<Card>().type = CardType.Spell;    
    }
    
}
