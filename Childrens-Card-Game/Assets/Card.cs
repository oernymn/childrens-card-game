

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    [HideInInspector]
    public string parentName;
    public int index;
    [HideInInspector]
    protected Transform cardsFunctionsEtc;
    [HideInInspector]
    protected Transform everything;
    [HideInInspector]
    protected Transform everythingBefore;
    [HideInInspector]
    protected Transform hand1;
    [HideInInspector]
    protected Transform hand2;
    [HideInInspector]
    protected Transform deck1;
    [HideInInspector]
    protected Transform deck2;
    [HideInInspector]
    protected Transform board1;
    [HideInInspector]
    protected Transform board2;

    protected cardEffectFunctions Functions;

    private void Awake()
    {
        
        everything = GameObject.Find("everything").transform;
        everythingBefore = GameObject.Find("everythingBefore").transform;

        hand1 = GameObject.Find("hand1").transform;
        hand2 = GameObject.Find("hand2").transform;
        deck1 = GameObject.Find("deck1").transform;
        deck2 = GameObject.Find("deck2").transform;
        board1 = GameObject.Find("board1").transform;
        board2 = GameObject.Find("board2").transform;

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
