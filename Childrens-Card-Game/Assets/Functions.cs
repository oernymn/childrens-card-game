using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;

public class EffectEventArgs : EventArgs
{
    public Card before { get; set; }
    public Card after { get; set; }

    public List<Card> BeforeList;
    public List<Card> AfterList;

    public EffectEventArgs(Card Before, Card After)
    {
        before = Before;
        after = After;    
    }
    public EffectEventArgs(List<Card> Before, List<Card> After)
    {
        BeforeList = Before;
        AfterList = After;
    }
}

public class Functions : MonoBehaviour
{
   
   static public event EventHandler<EffectEventArgs> runAfterEffects;
   static public event EventHandler<EffectEventArgs> runWheneverEffects;

   

    static public Card RunEffects(Card after, Card affecter, Action<Card, Card> CardEffect,   bool update = true)
    {
        Card before = SetBefore(after);
        after.affecter = affecter;

        CardEffect(after, affecter);
        RunWheneverEffects(before, after);
        // Applies the effect after the whenever effects have triggered.
        CardEffect(after, affecter);
        before = RunAfterEffects(before, after, update);
        return before;
    }

    // psuedo overload to make it easier to make individual Cards.  
   static public Card RunWheneverEffects(Card before, Card after)
    {
        // Makes copies of after to signal what is going to happen.
        Card Becoming = SetBefore(after);


        // Revert after. Now we have what is going to be after the effect has happened (becomingAfter) and what is before the effect happened (after, before in WheneverCardEffect).
        after = RevertTo(before, after);

        Debug.Log("WheneverEffects, Board: " + after.transform.parent + "Cards: " + after.transform.parent.childCount);

        EffectEventArgs becomingEventArg = new EffectEventArgs(after, Becoming);
        runWheneverEffects(after, becomingEventArg);

        Destroy(Becoming.gameObject);

        return after;
    }

    static public void RunWheneverEffects(List<Card> BeforeList, List<Card> AfterList)
    {
        // Makes copies of after to signal what is going to happen.
        List<Card> Becoming = SetBefore(AfterList);


        // Revert after. Now we have what is going to be after the effect has happened (becomingAfter) and what is before the effect happened (after, before in WheneverCardEffect).

        AfterList = RevertTo(BeforeList);

        EffectEventArgs becomingBeforeAfter = new EffectEventArgs(AfterList, Becoming);
        runWheneverEffects(AfterList, becomingBeforeAfter);

        foreach ( Card Card in Becoming)
        {
            Destroy(Card.gameObject);
        }
    }

    static private Card RevertTo(Card before, Card oldAfter)
    {
        // Need to change parent because destruction is too slow.
        oldAfter.transform.parent = everythingBefore;
        Destroy(oldAfter.gameObject);

        Card after = Instantiate(before);
        // Get rid of the (Clone) text.
        after.name = after.name.Replace("(Clone)", "");
        after.transform.parent = before.GetComponent<Card>().Container;
        after.transform.SetSiblingIndex(before.Index);
        after.gameObject.SetActive(true);
        return after;
    }

    static private List<Card> RevertTo(List<Card> BeforeList)
    {
        List<Card> AfterList = new List<Card>();

        foreach (Card Card in BeforeList)
        {

            Card after = Instantiate(Card);
            after.transform.parent = Card.GetComponent<Card>().Container;
            after.transform.SetSiblingIndex(Card.GetComponent<Card>().Index);
            after.gameObject.SetActive(true);
            AfterList.Add(after);
            
        }
        return AfterList;
    }

    static public Card RunAfterEffects(Card before, Card after, bool update = true)
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

    static public List<Card> RunAfterEffects(List<Card> BeforeList, List<Card> AfterList, bool update = true)
    {
        EffectEventArgs beforeAfter = new EffectEventArgs(BeforeList, AfterList);

        runAfterEffects(AfterList, beforeAfter);

        if (update == true)
        {
            updateAll();
        }

        foreach ( Card Card in BeforeList)
        {
            Destroy(Card.gameObject);
        }
        
        BeforeList = SetBefore(AfterList);
        return BeforeList;
    }

    static public Card SetBefore(Card after)
    {

       
            // Registers previous parent and index.
            after.Allegiance = after.transform.parent.parent;
            after.Container = after.transform.parent;
            after.Index = after.transform.GetSiblingIndex();
            // Need to set After inactive because otherwise 'before' will run its effects.
            after.gameObject.SetActive(false);
            Card before = Instantiate(after.transform, everythingBefore).GetComponent<Card>();
            after.gameObject.SetActive(true);
        // Get rid of the (Clone) text.
        before.name = before.name.Replace("(Clone)", "");

        return before;
    }

    static public List<Card> SetBefore(List<Card> AfterList)
    {
        List<Card> BeforeList = new List<Card>();

        foreach (Card card in AfterList)
        {

            // Registers previous parent and index.
            card.Allegiance = card.transform.parent;
            card.Container = card.transform.parent;
            card.Index = card.transform.GetSiblingIndex();
            // Need to set Card inactive because otherwise 'before' will run its effects.
            card.gameObject.SetActive(false);
            Card before = Instantiate(card, everythingBefore);
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

   static public Transform GetWhatIsMousedOver()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            return hit.transform;

        }
        else
        {
            return null;
        }
    }


    // draw the Card at the indexes
    static public void TransferCard(Card caller, Transform targetContainer, Transform source, int[] indexes)
    {
        List<Card> AfterList = new List<Card>();
        List<Card> BeforeList = new List<Card>();

        for (int i = 0; i < indexes.Length; i++)
        {

            // if the deck doesn't have enough Cards to contain the Card at indexes[i] -i then send error message.
            if (source.childCount < indexes[i] - i + 1)
            {

                Debug.Log("No Cards left");

            }
            else

            {
                // if the deck does have enough Cards: Identifies the Cards as they were before and after the effect.
                // -i to prevent index inflation from movement
                // Need to create a copy of everything because the assignment only creates a reference.

                // Single item list. Will be deleted after each iteration.

                Card after;
                Card before;

                after = source.GetChild(indexes[i] - i).GetComponent<Card>();
                before = SetBefore(after);

                AfterList.Add(after);
                BeforeList.Add(before);

                // Transfers the Card to the target location. Need to call whenever effect etc manually because the effect delegate can only have one argument.
                Draw(caller, targetContainer, after);
                RunWheneverEffects(before, after);
                Draw(caller, targetContainer, after);
                before = RunAfterEffects(before, after);

                ReturnToNeutral(caller, after);
                RunWheneverEffects(before, after);
                ReturnToNeutral(caller, after);
                before = RunAfterEffects(before, after);

            }
        }

        // Already applied effects on the Cards in the list.

        RunWheneverEffects(BeforeList, AfterList);

        foreach (Card card in AfterList)
        {
            Draw(caller, targetContainer, card);
        }

        BeforeList = RunAfterEffects(BeforeList, AfterList);

    }

    private static void Draw(Card caller, Transform targetContainer, Card after )
    {
        after.status = Status.BeingDrawn;
        after.targeter = caller.GetComponent<Card>();
        after.transform.parent = targetContainer;
    }

    private static void ReturnToNeutral(Card caller, Card after)
    {
        after.status = Status.Neutral;
        caller.GetComponent<Card>().targeter = null;
    }

    
}