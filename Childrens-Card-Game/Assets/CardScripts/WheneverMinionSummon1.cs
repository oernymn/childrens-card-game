using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;
using static GetSet;

public class WheneverMinionRetarget : Card
{


    public override void WheneverCardEffect(object sender, EffectEventArgs e)
    {

        // When a card is played.
        for (int i = 0; i < e.AfterList.Count; i++)
        {

            if (e.BeforeList[i].target == null && e.AfterList[i].target != null)
            {
                Card Target = e.BeforeList[i].target;
                ChangedAffectedList = e.BeforeList;

                ChangedAffectedList[ChangedAffectedList.Count - 1] = Target.transform.parent.GetChild(Target.transform.GetSiblingIndex() + 1).GetComponent<Card>();



                List<Card> AffectedList = RunEffects(e.BeforeList, ChangeTarget);


                //   RunEffects(new List<Card> { })

            }
        }
    }


    public void ChangeTarget (List<Card> AffectedList)
    {
       
      

    }


}
