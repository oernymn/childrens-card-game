using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;


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

    static public event EventHandler<EffectEventArgs> runAfterEffects;
    static public event EventHandler<EffectEventArgs> runWheneverEffects;


    // psuedo overload to make it easier to make individual Cards.  
    static public List<Card> RunEffects(List<Card> AfterList, Action<List<Card>> CardEffect, bool update = true)
    {

        List<Card> BeforeList = SetBefore(AfterList);

        CardEffect(AfterList);
        AfterList = RunWheneverEffects(BeforeList, AfterList);
        // Applies the effect after the whenever effects have triggered.
        CardEffect(AfterList);

        AfterList = RunAfterEffects(BeforeList, AfterList);



        ReturnToNeutral(AfterList);

        return AfterList;
    }

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
    
 
    static private List<Card> RevertTo(List<Card> BeforeList, List<Card> OldAfterList)
    {
        List<Card> AfterList = new List<Card>();

        for (int i = 0; i < BeforeList.Count; i++ )
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
          //  Debug.Log("Destroying " + OldAfterList[i]);

            OldAfterList[i].transform.parent = everythingBefore;
            Destroy(OldAfterList[i].gameObject);
        }

        return AfterList;
    }
    
    static public List<Card> RunAfterEffects(List<Card> BeforeList, List<Card> AfterList, bool update = true)
    {
/*
        for (int i = 0; i < AfterList.Count; i++)
        {
            Debug.Log($"AfterList[{i}]: {AfterList[i]}");
            Debug.Log($"BeforeList[{i}]: {BeforeList[i]}");
        }
        */
        EffectEventArgs beforeAfter = new EffectEventArgs(BeforeList, AfterList);

        runAfterEffects(1, beforeAfter);

        if (update == true)
        {
            updateAll();
        }

        foreach (Card card in BeforeList)
        {
            Debug.Log("Destroying " + card.name + " in BeforeList");
            Destroy(card.gameObject);
        }

        return AfterList;
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

            card.gameObject.SetActive(false);
            Card before = Instantiate(card, everythingBefore);
            card.gameObject.SetActive(true);

            // Rename to get rid of the (Clone) text.
            before.name = before.name.Replace("(Clone)", "");
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

    static public void SetTarget(Card targeter, Card target)
    {
        targeter.target = target;
        target.targeter = targeter;

    }


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

                List<Card> AfterList = new List<Card> { source.GetChild(indexes[i] - i).GetComponent<Card>(), caller, targetContainer.GetComponent<Card>() };

                RunEffects(AfterList, Move);
                RunEffects(AfterList, ReturnToNeutral);

            }
        }

    }

    private static void Move(List<Card> AfterList)
    {
        Card card = AfterList[0];
        Card caller = AfterList[1];
        Transform targetContainer = AfterList[2].transform;

        SetTarget(caller, card);
        card.transform.parent = targetContainer;
    }

    private static void ReturnToNeutral(List<Card> AfterList)
    {
        foreach (Card card in AfterList)
        {
            card.status = Status.Neutral;
            card.target = card.targeter =
            card.affects = card.affecter =
            null;
        }         
    }


}