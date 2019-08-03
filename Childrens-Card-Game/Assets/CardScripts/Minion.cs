using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Card
{
    /*
    public override int manaCost { get; set; } = 4;
    public override int baseAttack { get; set; } = 4;
    public override int attack { get; set; } = 4;
    public override int baseHealth { get; set; } = 4;
    public override int health { get; set; } = 4;
    public override int maxHealth { get; set; } = 4;
    */
    
    


    public override void AfterCardEffect(object sender, EffectEventArgs e)
    {


    }


    private void Awake()
    {
        health = 4;
    }


}
