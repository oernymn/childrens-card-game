

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    /*
    public virtual int manaCost { get; set; };
    public virtual int baseAttack { get; set; }
    public virtual int attack { get; set; }
    public virtual int baseHealth { get; set; }
    public virtual int health { get; set; }
    public virtual int maxHealth { get; set; }
    */

   
    public int health;



    [HideInInspector]
    public int board1Index = 0;
    [HideInInspector]
    public int board2Index = 1;
    [HideInInspector]
    public int handIndex = 2;
    [HideInInspector]
    public int deckIndex = 3;
    [HideInInspector]
    public int graveyardIndex = 4;

    public Transform Parent;
    public int index;


    Transform everything;
    Transform everythingBefore;
    Transform cardsFunctionsEtc;
    protected cardEffectFunctions Functions;

    private void Awake()
    {
        
        everything = GameObject.Find("everything").transform;
        everythingBefore = GameObject.Find("everythingBefore").transform;

        cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
        Functions = cardsFunctionsEtc.GetComponent<cardEffectFunctions>();

        // subscribes to runEffects
        Functions.runEffects += CardEffect;
    }

    public Status status;
    public Type type;
    public Transform target;
    public Transform targeter;

    public virtual void CardEffect(object sender, EffectEventArgs e) { }
    public virtual List<Transform> GetSelection() {
        return null;
    }

    public enum Status
    {
        Neutral,
        Attacking,
        Defending,
        BeingBounced,
        BeingDrawn,
        BeingPlayed
    }

    public enum Type
    {
        Minion,
        Enchantment,
        Spell
    }



    

}
