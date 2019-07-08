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

        // hand1.GetChild(0).transform.localScale = Vector3.zero;
    }


    public class Location
    {
        public int index;
        public Transform parent;
    }




    private void updateAll()
    {
        hand1.GetComponent<hand>().updateHand();
        hand2.GetComponent<hand>().updateHand();

    }


    public void runEffects(Transform before, Transform after)
    {
        Debug.Log($"before parent: {before.transform.parent}");
        Debug.Log($"after parent: {after.transform.parent}");

        foreach (Transform collection in everything)
        {
            foreach (Transform card in collection)
            {
                // card.GetComponent<cardEffect>().
            }
        }

    }
    // draw the card at the indexes
    public void TransferCard(Transform target, Transform source, int[] indexes)
    {

        for (int i = 0; i < indexes.Length; i++)
        {

            // when you move the card from the deck it changes the index so you basically inflate the index each iteration, so you conpensate by reducing the target index by i.
            // 0 *1* 2 3 4      
            // 1 2 *3* 4 
            // 2 3 4 *5* 6
            // vs      
            // 0 *1* 2 3
            // 1 *2* 3 4   3 - 1 = 2
            // 2 *3* 4 5   5 - 2 = 3

            // copies everything in everythingBefore and makes everythingBefore invisible.
            Transform everythingBefore = Instantiate(everything);
            everythingBefore.localScale = Vector3.zero;


            Transform before = everythingBefore.Find(source.name).GetChild(indexes[i]);


            // links after to the object
            Transform after = source.GetChild(indexes[i] - i);

            after.transform.parent = target;

            runEffects(before, after);

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