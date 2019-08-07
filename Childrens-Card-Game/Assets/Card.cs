

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;


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

    // For the 'before' cards.
    public Transform Allegiance;
    public Transform Container;
    public int Index;

    public Transform board1;
    public Transform board2;
    public Transform hand;
    public Transform deck;

    public Status status;
    public CardType type;
    public Transform target;
    public Transform targeter;


    public virtual void AfterCardEffect(object sender, EffectEventArgs e) { }
    public virtual void WheneverCardEffect(object sender, EffectEventArgs e) { }


    public virtual List<Transform> GetTargets()
    {
        return null;
    }

    private void Awake()
    {    


        // Subscribes to runEffects
        runAfterEffects += AfterCardEffect;
        runWheneverEffects += WheneverCardEffect;

        // Registers this card's side's containers.
        hand = transform.parent.parent.GetChild(handIndex);
        board1 = transform.parent.parent.GetChild(board1Index);
        board2 = transform.parent.parent.GetChild(board2Index);
        deck = transform.parent.parent.GetChild(deckIndex);

        HashSet<Status> Is = new HashSet<Status> { Status.Neutral };

    }

}








