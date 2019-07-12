using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class Card : MonoBehaviour
{


    public abstract void CardEffect(Transform before, Transform after);
    
    
}


public abstract class Minion : Card
{

    
    public abstract int Atk { get; set; }
   public enum Status
    {
        Attacking,
        Defending,
        BeingBounced,

    }
}



    