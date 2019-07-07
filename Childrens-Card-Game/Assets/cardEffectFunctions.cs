using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardEffectFunctions : MonoBehaviour
{
    public GameObject allCards;
    public Transform hand1;
    public Transform hand2;
    public Transform deck1;
    public Transform deck2;

    // Start is called before the first frame update
    void Start()
    {

        Draw(1, 5);
    }


    private void updateAll()
    {
        hand1.GetComponent<hand>().updateHand();
        hand2.GetComponent<hand>().updateHand();
        
    }


    // draw the card at the indexes
    public void TransferCard(Transform target, Transform deck, int[] indexes)
    {



        for (int i = 0; i < indexes.Length; i++)
        {

            // when you move the card from the deck it changes the index so you basically inflate the index each iteration, so you conpensate by reducing the target index by i
            // 0 *1* 2 3 4      
            // 1 2 *3* 4 
            // 2 3 4 *5* 6
            // vs      
            // 0 *1* 2 3
            //  1 *2* 3 4   3 - 1 = 2
            //  2 *3* 4 5   5 - 2 = 3

            deck.GetChild(indexes[i] - i).transform.parent = target;
           
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