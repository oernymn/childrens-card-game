

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
    public Transform cardsFunctionsEtc;
    [HideInInspector]
    public Transform everything;
    [HideInInspector]
    public Transform everythingBefore;
    [HideInInspector]
    public Transform hand1;
    [HideInInspector]
    public Transform hand2;
    [HideInInspector]
    public Transform deck1;
    [HideInInspector]
    public Transform deck2;
    [HideInInspector]
    public Transform board1;
    [HideInInspector]
    public Transform board2;

    protected cardEffectFunctions Functions;

    private void Awake()
    {
        cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
        everything = GameObject.Find("everything").transform;
        everythingBefore = GameObject.Find("everythingBefore").transform;

        hand1 = GameObject.Find("hand1").transform;
        hand2 = GameObject.Find("hand2").transform;
        deck1 = GameObject.Find("deck1").transform;
        deck2 = GameObject.Find("deck2").transform;
        board1 = GameObject.Find("board1").transform;
        board2 = GameObject.Find("board2").transform;

        Functions = cardsFunctionsEtc.GetComponent<cardEffectFunctions>();

        Functions.runEffects += CardEffect;
    }

    public Status status;
    public Transform target;
    public Transform targeter;

    public virtual void CardEffect(object sender, EffectEventArgs e) { }

    public enum Status
    {
        Neutral,
        Attacking,
        Defending,
        BeingBounced,
        BeingDrawn,

    }

    

}
