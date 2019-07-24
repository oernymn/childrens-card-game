using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Update : MonoBehaviour
{
    public float margin;
    public int maxHandSize;

    public Transform hand1;
    public Transform hand2;
    public Transform board1;
    public Transform board2;


    // puts the cards in the right position
    public void updateHand(Transform hand)
    {

        int cardsInHand = hand.childCount;
        int i = 0;

        foreach (Transform child in hand)
        {
            // set the position of the leftmost card
            float leftMostPosition = hand.position.x - (margin / 2 * (cardsInHand - 1));
            // position every card after that a card's length to the right of eachother 
            float x = leftMostPosition + (margin * i);
            // stack them on top of eachother
            float y = hand.position.y + 0.01f * i;
            Vector3 p = new Vector3(x, y, hand.position.z);
            child.transform.position = p;

            i++;
        }

    }

    public void updateBoard(Transform board)
    {
        int cardsOnBoard = board.childCount;
        int i = 0;

        foreach (Transform child in board)
        {
            // set the position of the leftmost card
            float leftMostPosition = board.position.x - (margin / 2 * (cardsOnBoard - 1));
            // position every card after that a card's length to the right of eachother 
            float x = leftMostPosition + (margin * i);
            // stack them on top of eachother
            Vector3 p = new Vector3(x, 0, board.position.z);
            child.transform.position = p;
          

            i++;
        }
    }
}



