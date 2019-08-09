using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;

public class EffectEventArgs : EventArgs
{
    public Transform before { get; set; }
    public Transform after { get; set; }

    public List<Transform> BeforeList;
    public List<Transform> AfterList;

    public EffectEventArgs(Transform Before, Transform After)
    {
        before = Before;
        after = After;    
    }
    public EffectEventArgs(List<Transform> Before, List<Transform> After)
    {
        BeforeList = Before;
        AfterList = After;
    }
}

public class Functions : MonoBehaviour
{
   
   static public event EventHandler<EffectEventArgs> runAfterEffects;
   static public event EventHandler<EffectEventArgs> runWheneverEffects;

   

    static public Transform RunEffects(Transform before, Transform after, Action<Transform> CardEffect, bool update = true)
    {
        RunWheneverEffects(before, after);
        // Applies the effect after the whenever effects have triggered.
        CardEffect(after);

        before = RunAfterEffects(before, after, update);
        return before;
    }

    // psuedo overload to make it easier to make individual cards.  
   static public Transform RunWheneverEffects(Transform before, Transform after)
    {
        // Makes copies of after to signal what is going to happen.
        Transform Becoming = SetBefore(after);


        // Revert after. Now we have what is going to be after the effect has happened (becomingAfter) and what is before the effect happened (after, before in WheneverCardEffect).
        after = RevertTo(before, after);

        Debug.Log("WheneverEffects, Board: " + after.parent + "Cards: " + after.parent.childCount);

        EffectEventArgs becomingEventArg = new EffectEventArgs(after, Becoming);
        runWheneverEffects(after, becomingEventArg);

        Destroy(Becoming.gameObject);

        return after;
    }

    static public void RunWheneverEffects(List<Transform> BeforeList, List<Transform> AfterList)
    {
        // Makes copies of after to signal what is going to happen.
        List<Transform> Becoming = SetBefore(AfterList);


        // Revert after. Now we have what is going to be after the effect has happened (becomingAfter) and what is before the effect happened (after, before in WheneverCardEffect).

        AfterList = RevertTo(BeforeList);

        EffectEventArgs becomingBeforeAfter = new EffectEventArgs(AfterList, Becoming);
        runWheneverEffects(AfterList, becomingBeforeAfter);

        foreach ( Transform card in Becoming)
        {
            Destroy(card.gameObject);
        }
    }

    static private Transform RevertTo(Transform before, Transform oldAfter)
    {
        // Need to change parent because destruction is too slow.
        oldAfter.parent = everythingBefore;
        Destroy(oldAfter.gameObject);

        Transform after = Instantiate(before);
        // Get rid of the (Clone) text.
        after.name = after.name.Replace("(Clone)", "");
        after.parent = before.GetComponent<Card>().Container;
        after.SetSiblingIndex(before.GetComponent<Card>().Index);
        after.gameObject.SetActive(true);
        return after;
    }

    static private List<Transform> RevertTo(List<Transform> BeforeList)
    {
        List<Transform> AfterList = new List<Transform>();

        foreach (Transform card in BeforeList)
        {

            Transform after = Instantiate(card);
            after.parent = card.GetComponent<Card>().Container;
            after.SetSiblingIndex(card.GetComponent<Card>().Index);
            after.gameObject.SetActive(true);
            AfterList.Add(after);
            
        }
        return AfterList;
    }

    static public Transform RunAfterEffects(Transform before, Transform after, bool update = true)
    {
        EffectEventArgs beforeAfter = new EffectEventArgs(before, after);

        runAfterEffects(after, beforeAfter);

        Destroy(before.gameObject);
        before = SetBefore(after);

        if (update == true)
        {
            updateAll();
        }

        return before;
    }

    static public List<Transform> RunAfterEffects(List<Transform> BeforeList, List<Transform> AfterList, bool update = true)
    {
        EffectEventArgs beforeAfter = new EffectEventArgs(BeforeList, AfterList);

        runAfterEffects(AfterList, beforeAfter);

        if (update == true)
        {
            updateAll();
        }

        foreach ( Transform card in BeforeList)
        {
            Destroy(card.gameObject);
        }
        
        BeforeList = SetBefore(AfterList);
        return BeforeList;
    }

    static public Transform SetBefore(Transform after)
    {

       
            // Registers previous parent and index.
            after.GetComponent<Card>().Allegiance = after.parent.parent;
            after.GetComponent<Card>().Container = after.parent;
            after.GetComponent<Card>().Index = after.GetSiblingIndex();
            // Need to set After inactive because otherwise 'before' will run its effects.
            after.gameObject.SetActive(false);
            Transform before = Instantiate(after, everythingBefore);
            after.gameObject.SetActive(true);
        // Get rid of the (Clone) text.
        before.name = before.name.Replace("(Clone)", "");

        return before;
    }

    static public List<Transform> SetBefore(List<Transform> AfterList)
    {
        List<Transform> BeforeList = new List<Transform>();

        foreach (Transform card in AfterList)
        {

            // Registers previous parent and index.
            card.GetComponent<Card>().Allegiance = card.parent;
            card.GetComponent<Card>().Container = card.parent;
            card.GetComponent<Card>().Index = card.GetSiblingIndex();
            // Need to set card inactive because otherwise 'before' will run its effects.
            card.gameObject.SetActive(false);
            Transform before = Instantiate(card, everythingBefore);
            card.gameObject.SetActive(true);
            // Rename to get rid of the (Clone) text.
            before.name = card.name;
            BeforeList.Add(before);
        }
        return BeforeList;
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

    static public Transform GetEnemyContainer(Transform container)
    {
        Transform enemy;

        if (container.parent == Alliance)
        {
            enemy = Horde.GetChild(container.GetSiblingIndex());
        } else 
        if (container.parent == Horde)
        {
            enemy = Alliance.GetChild(container.GetSiblingIndex());
        } else
        {
            return null;
        }
        return enemy;
    }


    // draw the card at the indexes
    static public void TransferCard(Transform caller, Transform targetContainer, Transform source, int[] indexes)
    {
        List<Transform> AfterList = new List<Transform>();
        List<Transform> BeforeList = new List<Transform>();

        for (int i = 0; i < indexes.Length; i++)
        {

            // if the deck doesn't have enough cards to contain the card at indexes[i] -i then send error message.
            if (source.childCount < indexes[i] - i + 1)
            {

                Debug.Log("No cards left");

            }
            else

            {
                // if the deck does have enough cards: Identifies the cards as they were before and after the effect.
                // -i to prevent index inflation from movement
                // Need to create a copy of everything because the assignment only creates a reference.

                // Single item list. Will be deleted after each iteration.

                Transform after;
                Transform before;

                after = source.GetChild(indexes[i] - i);
                before = SetBefore(after);

                AfterList.Add(after);
                BeforeList.Add(before);

                Card afterCard = after.GetComponent<Card>();

                // Transfers the card to the target location. Need to call whenever effect etc manually because the effect delegate can only have one argument.
                Draw(caller, targetContainer, after, afterCard);
                RunWheneverEffects(before, after);
                Draw(caller, targetContainer, after, afterCard);
                before = RunAfterEffects(before, after);

                ReturnToNeutral(caller, afterCard);
                RunWheneverEffects(before, after);
                ReturnToNeutral(caller, afterCard);
                before = RunAfterEffects(before, after);

            }
        }

        // Already applied effects on the cards in the list.

        RunWheneverEffects(BeforeList, AfterList);

        foreach (Transform card in AfterList)
        {
            Card afterCard = card.GetComponent<Card>();
            Draw(caller, targetContainer, card, afterCard);
        }

        BeforeList = RunAfterEffects(BeforeList, AfterList);

    }

    private static void Draw(Transform caller, Transform targetContainer, Transform after, Card afterCard)
    {
        afterCard.status = Status.BeingDrawn;
        afterCard.targeter = caller;
        after.transform.parent = targetContainer;
    }

    private static void ReturnToNeutral(Transform caller, Card afterCard)
    {
        afterCard.status = Status.Neutral;
        caller.GetComponent<Card>().targeter = null;
    }

    
}