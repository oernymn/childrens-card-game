using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Minion : Card
{



    public override void AfterCardEffect(object sender, EffectEventArgs e)
    {


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
