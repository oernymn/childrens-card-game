using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;


public class EffectEventArgs : EventArgs
{
    /*
    public Card before { get; set; }
    public Card after { get; set; }

    public EffectEventArgs(Card Before, Card After)
    {
        before = Before;
        after = After;
    }
    */
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

    static public event EventHandler<EffectEventArgs> runAfterEffects;
    static public event EventHandler<EffectEventArgs> runWheneverEffects;



    static public List<Card> RunEffects(List<Card> AfterList, Action<List<Card>> CardEffect, bool update = true)
    {
        List<Card> BeforeList = SetBefore(AfterList);

        CardEffect(AfterList);
        AfterList = RunWheneverEffects(BeforeList, AfterList);
        // Applies the effect after the whenever effects have triggered.
        CardEffect(AfterList);

        RunAfterEffects(BeforeList, AfterList);

        return AfterList;
    }

    // psuedo overload to make it easier to make individual Cards.  
    /*
    static public Card RunWheneverEffects(Card before, Card after)
    {
        // Makes copies of after to signal what is going to happen.
        Card Becoming = SetBefore(after);


        // Revert after. Now we have what is going to be after the effect has happened (becomingAfter) and what is before the effect happened (after, before in WheneverCardEffect).
        after = RevertTo(before, after);

        EffectEventArgs becomingEventArg = new EffectEventArgs(after, Becoming);
        runWheneverEffects(1, becomingEventArg);

        Destroy(Becoming.gameObject);

        return after;
    }
    */
    static public List<Card> RunWheneverEffects(List<Card> BeforeList, List<Card> AfterList)
    {
        // Makes copies of after to signal what is going to happen.
        List<Card> Becoming = SetBefore(AfterList);

        // Revert after. Now we have what is going to be after the effect has happened (Becoming) and what is before the effect happened (AfterList, before in WheneverCardEffect).

        AfterList = RevertTo(BeforeList, AfterList);

        EffectEventArgs BeforeAfter = new EffectEventArgs(AfterList, Becoming);
        runWheneverEffects(1, BeforeAfter);


        foreach (Card card in Becoming)
        {
            Destroy(card.gameObject);
        }
        // If you don't return it, it will not revert. 
        return AfterList;
    }
    /*
    static private Card RevertTo(Card before, Card oldAfter)
    {
        // Need to change parent because destruction is too slow.
        oldAfter.transform.parent = everythingBefore;
        Destroy(oldAfter.gameObject);
        // Need to set this otherwise it will reset the values in the Start() function.
        before.alreadySet = true;
        Card after = Instantiate(before);
        // Get rid of the (Clone) text.
        after.name = after.name.Replace("(Clone)", "");
        after.transform.parent = before.Container;
        after.transform.SetSiblingIndex(before.Index);
        after.gameObject.SetActive(true);
        return after;
    }
    */
    static private List<Card> RevertTo(List<Card> BeforeList, List<Card> OldAfterList)
    {
        List<Card> AfterList = new List<Card>();

        for (int i = 1; i < BeforeList.Count; i++ )
        {

            // Need to change parent because destruction is too slow.
            OldAfterList[i].transform.parent = everythingBefore;
            Destroy(OldAfterList[i].gameObject);

            Card card = BeforeList[i];

            // Need to set this otherwise it will reset the values in the Start() function.
            card.alreadySet = true;

            Card after = Instantiate(card);
            after.transform.parent = card.Container;
            after.transform.SetSiblingIndex(card.Index);
            after.gameObject.SetActive(true);
            AfterList.Add(after);

        }

        return AfterList;
    }
    /*
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
    */
    static public List<Card> RunAfterEffects(List<Card> BeforeList, List<Card> AfterList, bool update = true)
    {
        EffectEventArgs beforeAfter = new EffectEventArgs(BeforeList, AfterList);

        runAfterEffects(1, beforeAfter);

        if (update == true)
        {
            updateAll();
        }

        foreach (Card Card in BeforeList)
        {
            Destroy(Card.gameObject);
        }

        BeforeList = SetBefore(AfterList);
        return BeforeList;
    }
    /*
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
    */
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

    static public Transform GetContainer(Card sender, bool isEnemy, int ContainerIndex)
    {
        Transform container;

        if (sender.transform.parent.parent == Alliance)
        {
            if (isEnemy)
            {
                container = Horde.GetChild(ContainerIndex);
            }
            else
            {
                container = Alliance.GetChild(ContainerIndex);
            }
        }
        else
        {
            if (isEnemy)
            {
                container = Alliance.GetChild(ContainerIndex);
            }
            else
            {
                container = Horde.GetChild(ContainerIndex);
            }
        }
        return container;
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

    static 

    // draw the Card at the indexes
    static public void TransferCard(Card caller, Transform targetContainer, Transform source, int[] indexes)
    {

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

                List<Card> AfterList = new List<Card>();
                AfterList.Add(source.GetChild(indexes[i] - i).GetComponent<Card>());
                AfterList.Add(caller);
                AfterList.Add(targetContainer.GetComponent<Card>());

                RunEffects(AfterList, Draw);
                RunEffects(AfterList, ReturnToNeutral);

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

    private static void Draw(List<Card> AfterList)
    {
        Card after = AfterList[0];
        Card caller = AfterList[1];
        Transform targetContainer = AfterList[2].transform;

        after.status = Status.BeingDrawn;
        after.targeter = caller;
        after.transform.parent = targetContainer;
    }

    private static void ReturnToNeutral(Card caller, Card after)
    {
        after.status = Status.Neutral;
        caller.GetComponent<Card>().targeter = null;
    }


}