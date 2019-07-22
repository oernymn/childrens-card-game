using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

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

public class cardEffectFunctions : MonoBehaviour
{

    public Transform everything;
    public Transform everythingBefore;
    public Transform hand1;
    public Transform hand2;
    public Transform deck1;
    public Transform deck2;
    public Transform board1;
    public Transform board2;


    public event EventHandler<EffectEventArgs> runEffects;


    // psuedo overload to make it easier to make individual cards.
    public void RunEffects(Transform before, Transform after)
    {
        EffectEventArgs beforeAfter = new EffectEventArgs(before, after);
        runEffects(this, beforeAfter);
        updateAll();
        Destroy(before.gameObject);

    }

    public void Awake()
    {
        // Add CardEffects to the runEffects Event.
       // runEffects += this.GetComponent<Card>().CardEffect;
    }


    public Transform SetBefore(Transform after)
    {
        Transform before = Instantiate(after);
        before.gameObject.SetActive(false);
        before.gameObject.GetComponent<Card>().parentName = after.parent.name;
        return before;
    }



    private void updateAll()
    {
        hand1.GetComponent<hand>().updateHand();
        hand2.GetComponent<hand>().updateHand();

    }



    // draw the card at the indexes
    public void TransferCard(Transform caller, Transform target, Transform source, int[] indexes)
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
                before = SetBefore(after);
                before = SetBefore(after);
                before = SetBefore(after);
                before = SetBefore(after);
                before = SetBefore(after);
                before = SetBefore(after);
                before = SetBefore(after);
                before = SetBefore(after);

                before = SetBefore(after);

                after.GetComponent<Card>().status = Status.BeingDrawn;
                after.GetComponent<Card>().targeter = caller;
                caller.GetComponent<Card>().target = after;

                RunEffects(before, after);

                // then transfers the card to the target location, 
                after.transform.parent = target;
                after.GetComponent<Card>().status = Status.Neutral;
                after.GetComponent<Card>().targeter = null;
                caller.GetComponent<Card>().target = null;
                //  checks every other card if they have an effect that triggers off the transfer.

                RunEffects(before, after);


            }


        }

    }

}