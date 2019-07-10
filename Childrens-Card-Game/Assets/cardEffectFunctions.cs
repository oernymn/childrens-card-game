using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardEffectFunctions : MonoBehaviour
{

    public Transform everything;
    // public Transform everythingBefore;

    public Transform hand1;
    public Transform hand2;
    public Transform deck1;
    public Transform deck2;
    public Transform board;


    // Start is called before the first frame update
    void Start()
    {
        int[] indexes = { 0, 4, 6 };

        TransferCard(hand1, deck1, indexes);

        
    }


    // copies everything to everythingBefore and makes everythingBefore invisible.
    public Transform SetEverythingBefore()
    {
        Transform everythingBefore;
        // if everythingBefore doesn't exits it make it a copy of everything and renames itself to "everythingBefore"
        if (!(GameObject.Find("everythingBefore")))
        {
            everythingBefore = Instantiate(everything);
            everythingBefore.name = "everythingBefore";

        }
        else
        {
            everythingBefore = GameObject.Find("everythingBefore").transform;
        }

        everythingBefore.localScale = Vector3.zero;
        return everythingBefore;
    }



    private void updateAll()
    {
        hand1.GetComponent<hand>().updateHand();
        hand2.GetComponent<hand>().updateHand();

    }


    public void runEffects(Transform before, Transform after)
    {
        //Debug.Log($"before parent: {before.transform.parent}");
        //Debug.Log($"after parent: {after.transform.parent}");

        foreach (Transform collection in everything)
        {
            foreach (Transform card in collection)
            {
                card.GetComponent<CardEffect>().cardEffect(before, after);

              // Component Card = card.GetComponent("CardEffect");
              // Card.cardEffect(before, after);

            

            }
        }

    }
    // draw the card at the indexes
    public void TransferCard(Transform target, Transform source, int[] indexes)
    {

        for (int i = 0; i < indexes.Length; i++)
        {

            // when you move the card from the deck it changes the index so you basically inflate the index each iteration,
            // so you conpensate by reducing the target index by i.
            // 0 *1* 2 3 4      
            // 1 2 *3* 4 
            // 2 3 4 *5* 6
            // vs      
            // 0 *1* 2 3
            // 1 *2* 3 4   3 - 1 = 2
            // 2 *3* 4 5   5 - 2 = 3


            

            // if the deck doesn't have enough cards to contain the card at indexes[i] -i then send error message.
            if (source.childCount < indexes[i] - i + 1 ) {

                Debug.Log("No cards left");

            } else
                
               
            {

                // if the deck does have enough cards: Identifies the cards as they were before

                Transform everythingBefore = SetEverythingBefore();
                Transform before = everythingBefore.Find(source.name).GetChild(indexes[i] - i);

                // links 'after' to the card. When 'after' changes so will the card.
                Transform after = source.GetChild(indexes[i] - i);

                // then transfers the card to the target location, 
                after.transform.parent = target;
                //  checks every other card if they have an effect they triggers off of the transfer.
                runEffects(before, after);

            }

            updateAll();
        }



    }

    public void Draw(int allegiance, int cardToBeDrawn = 1)
    {
        Transform deck = deck1;
        Transform hand = hand1;
        if (allegiance == 1)
        {
            deck = deck1;
            hand = hand1;
        }
        else if (allegiance == 2)
        {
            deck = deck2;
            hand = hand2;
        }

        for (int i = 0; i < cardToBeDrawn; i++)
        {
            deck.GetChild(0).transform.parent = hand;


        }



    }
}