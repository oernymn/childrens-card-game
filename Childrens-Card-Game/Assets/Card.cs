

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Card : MonoBehaviour
{

    public Transform cardsFunctionsEtc;
    public Transform everything;
    public Transform hand1;
    public Transform hand2;
    public Transform deck1;
    public Transform deck2;
    public Transform board1;
    public Transform board2;

    public cardEffectFunctions Functions = new cardEffectFunctions();

    private void Awake()
    {
        cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
        everything = GameObject.Find("everything").transform;
        hand1 = GameObject.Find("hand1").transform;
        hand2 = GameObject.Find("hand2").transform;
        deck1 = GameObject.Find("deck1").transform;
        deck2 = GameObject.Find("deck2").transform;
        board1 = GameObject.Find("board1").transform;
        board2 = GameObject.Find("board2").transform;

        
    }

    private void Start()
    {
        int[] indexes = { 0, 3, 6 };
        Functions.TransferCard(this.transform, hand1, deck1, indexes);
    }


    public Status status;
    public Transform target;
    public Transform targeter;

    public virtual void CardEffect(object sender, EventArgs e) { }

    public enum Status
    {
        Neutral,
        Attacking,
        Defending,
        BeingBounced,
        BeingDrawn,

    }



}





/*
 private void Awake()
    {
     

    int[] indexes = { 0, 4, 6 };
    }*/ 
