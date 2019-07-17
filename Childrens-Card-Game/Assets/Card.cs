

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Card : MonoBehaviour
{

    public class ListEventArgs : EventArgs
    {
        public List<int> Data { get; set; }
        public ListEventArgs(List<int> data)
        {
            Data = data;
        }
    }


    // public delegate void (Transform before, Transform after);

    public delegate void EventHandler(int before, int after);
    public static event EventHandler<ListEventArgs> runEffects;



    public virtual void CardEffect(object sender, ListEventArgs e) {
    }
    public Status status;
    public Transform target;
    public Transform targeter;

    public void Awake()
    {

        runEffects += CardEffect;

        List<int> intList = new List<int> { 1, 4, 7 };

        ListEventArgs listArg = new ListEventArgs(intList);

        runEffects(this, listArg);
    }


    public enum Status
    {
        Neutral,
        Attacking,
        Defending,
        BeingBounced,
        BeingDrawn,

    }

  
}

public abstract class Minion : Card
{


    public abstract int Atk { get; set; }

}