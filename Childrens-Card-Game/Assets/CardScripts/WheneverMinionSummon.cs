using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;
using static GetSet;

public class WheneverMinionSummon : Card
{


    public override void WheneverCardEffect(object sender, EffectEventArgs e)
    {

        // When a card is played.
        for (int i = 0; i < e.AfterList.Count; i++)
        {

            if (e.BeforeList[i].status != Status.BeingPlayed && e.AfterList[i].status == Status.BeingPlayed)
            {
                Debug.Log("Whenever effect: " + e.AfterList[i].name + " played.");

                Transform newMinion = Instantiate(Collections.Minions["Minion"].transform);
                newMinion.name = "I'm new!";

                List<Card> AffectedList = RunEffects(new List<Card> { newMinion.GetComponent<Card>() }, Summon);


                //   RunEffects(new List<Card> { })

            }
        }
    }


    public void Summon (List<Card> AffectedList)
    {
       
        AffectedList[0].GetComponent<Stats>().SetStats(1, 1);
        AffectedList[0].Allegiance = Allegiance;
        SetContainer(AffectedList[0], true, board1Index);

    }


}
