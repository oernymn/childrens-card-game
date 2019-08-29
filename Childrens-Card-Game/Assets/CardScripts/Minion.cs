using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;


public class Minion : Card
{

    public new HashSet<KeyWord> KeyWords = new HashSet<KeyWord> { KeyWord.Taunt };

    public override void AfterCardEffect(object sender, EffectEventArgs e)
    {
        
        for (int i = 0; i < e.AfterList.Count; i++)
        {
            if (e.BeforeList[i].status != Status.BeingPlayed && e.AfterList[i].status == Status.BeingPlayed && e.AfterList[i] == this)
            {
                Debug.Log(name + " was played!");
            }
        }

    }

    private void Awake()
    {
        if (KeyWords.Contains(KeyWord.Taunt))
        {
            Debug.Log("AAAA");

        }
    }

}