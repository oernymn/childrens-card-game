using System;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static GetSet;

public class EffectEventArgs : EventArgs
{

    public List<Card> BeforeList;
    public List<Card> AfterList;


    public EffectEventArgs(List<Card> Before, List<Card> After)
    {
        BeforeList = Before;
        AfterList = After;
    }
}

public class Functions : MonoBehaviour
{
    static public event EventHandler<EffectEventArgs> runFinalEffects;
    static public event EventHandler<EffectEventArgs> runAfterEffects;
    static public event EventHandler<EffectEventArgs> runWheneverEffects;


    // psuedo overload to make it easier to make individual Cards.  
    static public List<Card> RunEffects(List<Card> AffectedList, Action<List<Card>> CardEffect, bool update = true)
    {
        AffectedList = SetInfo(AffectedList);
        List<Card> BeforeList = SetBefore(AffectedList);

        AffectedList = RunWheneverEffects(AffectedList, CardEffect);
        // Applies the effect after the whenever effects have triggered.
        CardEffect(AffectedList);

        AffectedList = RunAfterEffects(BeforeList, AffectedList);

        if (update == true)
        {
            updateAll();
        }

        AffectedList = RunFinalEffects(BeforeList, AffectedList);

        AffectedList = ReturnToNeutral(AffectedList);
        /*
        foreach (Card card in BeforeList)
        {
            Debug.Log("Destroying " + card.name + " in BeforeList");
            Destroy(card.gameObject);
        }
        */
        AffectedList = SetInfo(AffectedList);

        return AffectedList;
    }

    static public List<Card> RunWheneverEffects(List<Card> AffectedList, Action<List<Card>> CardEffect)
    {
        // Makes copies of AffectedList to signal what is going to happen.
        List<Card> Becoming = SetBefore(AffectedList);
        // Apply the effect.
        CardEffect(Becoming);

        EffectEventArgs BeforeAfter = new EffectEventArgs(AffectedList, Becoming);
        // Makes the static TheAffectedList equal to the normal one before whenevereffects are checked so the they can change the targets of the effects by
        // changing TheAffectedList
    //    ChangedAffectedList = AffectedList;
        runWheneverEffects(1, BeforeAfter);


        foreach (Card card in Becoming)
        {
            Destroy(card.gameObject);
        }
        // Returns the modified list.
        return AffectedList;
    }


 

    static public List<Card> RunAfterEffects(List<Card> BeforeList, List<Card> AfterList)
    {
        /*
        for (int i = 0; i < AfterList.Count; i++)
        {
            Debug.Log($"BeforeList[{i}]: {BeforeList[i]}");
            Debug.Log($"AfterList[{i}]: {AfterList[i]}");

        }
        */
        EffectEventArgs beforeAfter = new EffectEventArgs(BeforeList, AfterList);
        runAfterEffects(1, beforeAfter);
        return AfterList;
    }

    static public List<Card> RunFinalEffects(List<Card> BeforeList, List<Card> AfterList)
    {

        EffectEventArgs beforeAfter = new EffectEventArgs(BeforeList, AfterList);
        runFinalEffects(1, beforeAfter);
        return AfterList;
    }


    static public List<Card> SetBefore(List<Card> AfterList)
    {
        List<Card> BeforeList = new List<Card>();

        foreach (Card card in AfterList)
        {

            card.gameObject.SetActive(false);
            Card before = Instantiate(card, everythingBefore);
            card.gameObject.SetActive(true);

            // Rename to get rid of the (Clone) text.
            before.name = before.name.Replace("(Clone)", "");
            BeforeList.Add(before);
        }

        return BeforeList;
    }

    static public List<Card> SetInfo(List<Card> AfterList)
    {
        // Registers parent and index to use in whenever effects.

        foreach (Card card in AfterList)
        {


            if (card.transform.parent == null)
            {
                Debug.Log("No parent.");
                return AfterList;
            }

            // If it's a container, it's Allegiance is its parent.
            else if (card.transform.parent == Alliance || card.transform.parent == Horde)
            {
                card.Allegiance = card.transform.parent;
            }
            // If it's a card, it's Allegiance is its grandparent. 
            else if (card.transform.parent.parent == Alliance || card.transform.parent.parent == Horde)
            {
                card.Allegiance = card.transform.parent.parent;
            } 

            card.Container = card.transform.parent;
            card.Index = card.transform.GetSiblingIndex();
        }
        return AfterList;
    }

    static public void updateAll()
    {
        // Update all containers.
        foreach (Transform Allegiance in everything)
        {
            foreach (Transform container in Allegiance)
            {
                UpdateFunctions.update(container);
            }


        }
    }







}