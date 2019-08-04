using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;

public class EffectEventArgs : EventArgs
{
    public Transform before { get; set; }
    public Transform after { get; set; }

    public EffectEventArgs(Transform Before, Transform After)
    {
        before = Before;
        after = After;

        
    }
}



public class Functions : MonoBehaviour
{
   
   static public event EventHandler<EffectEventArgs> runAfterEffects;
   static public event EventHandler<EffectEventArgs> runWheneverEffects;

    static public Transform RunEffects(Transform before, Transform after, bool update = true)
    {
        RunWheneverEffects(before, after);
        // Applies the effect after the whenever effects have triggered.


        before = RunAfterEffects(before, after, update);
        return before;
    }

    // psuedo overload to make it easier to make individual cards.  
   static private void RunWheneverEffects(Transform before, Transform after)
    {
        // Makes copies of after to signal what is going to happen.
        Transform becomingAfter = SetBefore(after);


        // Revert after. Now we have what is going to be after the effect has happened (becomingAfter) and what is before the effect happened (after, before in WheneverCardEffect).

        after = Revert(before);

        EffectEventArgs becomingBeforeAfter = new EffectEventArgs(after, becomingAfter);
        runWheneverEffects(after, becomingBeforeAfter);

    }

   static private Transform Revert(Transform before)
    {
        Transform after = Instantiate(before);
        after.parent = before.GetComponent<Card>().Parent;
        after.SetSiblingIndex(before.GetComponent<Card>().index);
        after.gameObject.SetActive(true);
        return after;
    }

   static private Transform RunAfterEffects(Transform before, Transform after, bool update = true)
    {
        EffectEventArgs beforeAfter = new EffectEventArgs(before, after);

        runAfterEffects(after, beforeAfter);

        if (update == true)
        {
            updateAll();
        }
        Destroy(before.gameObject);
        before = SetBefore(after);
        return before;
    }

    static public Transform SetBefore(Transform after)
    {

        // Destroy all previous before-cards so they don't accumelate.
        
        foreach (Transform card in everythingBefore)
        {
            Destroy(card.gameObject);
        }
        
        // need to set After inactive because otherwise 'before' will run its effects.
        after.gameObject.SetActive(false);
        Transform before = Instantiate(after, everythingBefore);
        after.gameObject.SetActive(true);
        // Registers previous parent and index.
        before.gameObject.GetComponent<Card>().Parent = after.parent;
        before.gameObject.GetComponent<Card>().index = after.GetSiblingIndex();
        before.name = after.name;
        return before;
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

    static public Transform GetEnemyAllegiance(Transform grandParent)
    {
        Transform enemy;

        if (grandParent == Alliance)
        {
            enemy = Horde;
        } else 
        if (grandParent == Horde)
        {
            enemy = Alliance;
        } else
        {
            return null;
        }
        return enemy;
    }



    // draw the card at the indexes
    static public void TransferCard(Transform caller, Transform target, Transform source, int[] indexes)
    {

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


                Transform after = source.GetChild(indexes[i] - i);
                Transform before = SetBefore(after);

                after.GetComponent<Card>().status = Status.BeingDrawn;

                caller.GetComponent<Card>().target = after;

                before = RunEffects(before, after);

                // then transfers the card to the target location, 
                after.transform.parent = target;
                after.GetComponent<Card>().status = Status.Neutral;
                
                caller.GetComponent<Card>().target = null;
                //  checks every other card if they have an effect that triggers off the transfer.

                RunEffects(before, after);


            }


        }

    }

}