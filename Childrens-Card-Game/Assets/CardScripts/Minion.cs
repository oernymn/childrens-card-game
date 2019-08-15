using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;

public class Minion : Card
{



    public override void AfterCardEffect(object sender, EffectEventArgs e)
    {
        if ( e.before.target != this && e.after.target == this)
        {
            Debug.Log(name + " targeted.");
        }

    }


    private void Start()
    {
        if (alreadySet == false)
        {
            type = Variables.CardType.Minion;
            Debug.Log(name + " setting stats...");
            stats.baseHealth =
            stats.maxHealth =
            stats.currentHealth = 4;
            stats.attack = 2;
        }


    }


}
