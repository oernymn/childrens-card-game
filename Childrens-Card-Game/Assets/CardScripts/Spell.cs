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

            Transform target = e.after.GetComponent<Card>().target;
            Transform before = SetBefore(target);

            BattleCry(target);
            RunEffects(before, target, BattleCry);

        }
    }

    public void BattleCry(Transform after)
    {

        Card target = after.GetComponent<Card>();

        if (target.type == CardType.Minion)
        {

            Debug.Log($"From {target.health} health");
            target.health -= 2;
            Debug.Log($"To {target.health} health");
        }
    }

    public override List<Transform> GetTargets()
    {
       Transform enemyBoard1 = GetEnemy(board1);

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
