using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;

public class WheneverMinion : Card
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

                RunEffects(new List<Card> { newMinion.GetComponent<Card>() }, Summon);


                //   RunEffects(new List<Card> { })

            }
        }
    }


    public void Summon (List<Card> AffectedList)
    {
        foreach (Card card in AffectedList)
        {

        }
    }


}
