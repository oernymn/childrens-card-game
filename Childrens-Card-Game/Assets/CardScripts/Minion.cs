using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Minion : Card
{


    public override void AfterCardEffect(object sender, EffectEventArgs e)
    {


    }


    private void Awake()
    {
        type = Variables.CardType.Minion;
        GetComponent<Stats>().baseHealth =
        GetComponent<Stats>().maxHealth =
        GetComponent<Stats>().currentHealth = 4;
    }


}
