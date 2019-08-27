using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static GetSet;

public class BattleCryMinion : Card
{

    public override void AfterCardEffect(object sender, EffectEventArgs e)
    {

        // When a card is played.
        for (int i = 0; i < e.AfterList.Count; i++)
        {

            if (e.BeforeList[i].status != Status.BeingPlayed && e.AfterList[i].status == Status.BeingPlayed
                && e.AfterList[i] == this)
            {
                Debug.Log("BattleCry triggered");

                EffectTargeting.SetInfo(this, DamageTarget);

            }
        }
    }

    //  AffectedList[0] = Targeter
    //  AffectedList[1] = Target
    public void DamageTarget(List<Card> AffectedList)
    {
        Debug.Log(this.name + " deals 1 damage to " + AffectedList[1]);

        SetTarget(AffectedList[0], AffectedList[1]);

        //  SetTarget(, AffectedList[0]);
        AffectedList[1].stats.currentHealth -= 1;

    }


}
