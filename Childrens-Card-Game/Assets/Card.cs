

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static cardEffectFunctions;

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
    public Transform Parent;
    public int index;



    Transform everything;
    Transform everythingBefore;
    Transform cardsFunctionsEtc;
    protected cardEffectFunctions Functions;

   

    public Status status;
    public CardType type;
    public Transform target;

    

    public virtual void AfterCardEffect(object sender, EffectEventArgs e) { }
    public virtual void WheneverCardEffect(object sender, EffectEventArgs e) { }

    public virtual List<Transform> GetSelection()
    {
        return null;
    }

    private void Awake()
    {

        everything = GameObject.Find("everything").transform;
        everythingBefore = GameObject.Find("everythingBefore").transform;

        cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
        Functions = cardsFunctionsEtc.GetComponent<cardEffectFunctions>();

        // subscribes to runEffects
        Functions.runEffects += AfterCardEffect;
        Functions.runWheneverEffects += WheneverCardEffect;

        HashSet<Status> Is = new HashSet<Status> { Status.Neutral };

    }

}

   



    


