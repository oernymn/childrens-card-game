using System;
using System.Collections;
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

        AffectedList = RunWheneverEffects(BeforeList, AffectedList, CardEffect);
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

    static public List<Card> RunWheneverEffects(List<Card> BeforeList, List<Card> AfterList, Action<List<Card>> CardEffect)
    {
        // Makes copies of after to signal what is going to happen.
        List<Card> Becoming = SetBefore(AfterList);
        // Apply the effect.
        CardEffect(Becoming);

        EffectEventArgs BeforeAfter = new EffectEventArgs(AfterList, Becoming);
        runWheneverEffects(1, BeforeAfter);


        foreach (Card card in Becoming)
        {
            Destroy(card.gameObject);
        }
        
        return AfterList;
    }


    static private List<Card> RevertTo(List<Card> BeforeList, List<Card> OldAfterList)
    {
        List<Card> AfterList = new List<Card>();

        for (int i = 0; i < BeforeList.Count; i++)
        {
            Card card = BeforeList[i];

            // Need to set this otherwise it will reset the values in the Start() function.
            card.alreadySet = true;

            Card after = Instantiate(card);
            after.transform.parent = card.Container;
            after.transform.SetSiblingIndex(card.Index);
            after.gameObject.SetActive(true);
            after.name = after.name.Replace("(Clone)", "");

            AfterList.Add(after);

            // Need to change parent because destruction is too slow.
            Debug.Log("Destroying " + OldAfterList[i]);
            OldAfterList[i].name = "BadGuy";
            OldAfterList[i].transform.parent = everythingBefore;
            Destroy(OldAfterList[i].gameObject);
        }

        return AfterList;
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
            Debug.Log(card.name);

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
            // If it's a container...
            if (card.transform.parent == Alliance || card.transform.parent == Horde)
            {
                card.Allegiance = card.transform.parent;
            }
            else
            {
                card.Allegiance = card.transform.parent.parent;
            }

            Debug.Log($"Allegiance: {card.Allegiance}");

            card.Container = card.transform.parent;
            Debug.Log(card.Container.name);
            foreach( Transform child in card.Container)
            {
                Debug.Log($"Child name: {child.name}");
            }

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