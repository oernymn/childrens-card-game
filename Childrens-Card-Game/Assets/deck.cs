using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck : MonoBehaviour
{
    public GameObject allCards;
    public Transform hand1;
    public Transform hand2;

    // Start is called before the first frame update
    void Start()
    {

        // int[] newArray = { 2, 3, 6};

        // Draw(hand1, transform, newArray);
    }

   

    // draw a number of cards from a deck to a hand starting with the index.
    public void Draw(Transform hand, Transform deck, int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++) {

            // when you move the card from the deck it changes the index so you basically skip a card. so you take index[0] every time.
            // *0* 1 2 3 4      
            // 1 *2* 3 4 
            // 2 3 *4*
            // vs      
            // *0* 1 2 3
            // *1* 2 3 4
            // *2* 3 4 

            deck.GetChild(indexes[0]).transform.parent = hand;

        }   
      

        hand.GetComponent<hand>().updateHand();

    }
}