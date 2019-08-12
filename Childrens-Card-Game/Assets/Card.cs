

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;


public class Card : MonoBehaviour
{

    // For the 'before' cards.
    public Transform Allegiance;
    public Transform Container;
    public int Index;


    public Status status;
    public CardType type;
    HashSet<Tribe> Tribes = new HashSet<Tribe> { };

    public Component stats;

    public Card target;
    public Card targeter;
    public Card affected;
    public Card affecter;

    public virtual void AfterCardEffect(object sender, EffectEventArgs e) { }
    public virtual void WheneverCardEffect(object sender, EffectEventArgs e) { }


    public virtual List<Card> GetTargets()
    {
        return null;
    }

    private void Awake()
    {

        // Subscribes to runEffects
        runAfterEffects += AfterCardEffect;
        runWheneverEffects += WheneverCardEffect;

        
        

    }

}








