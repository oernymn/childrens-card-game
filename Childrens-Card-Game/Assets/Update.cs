﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Update : MonoBehaviour
{
    public float margin;
    public int maxHandSize;



    public void update(Transform container)
    {
        if (container.name == "board1" || container.name == "board2")
        {
            updateBoard(container);
        } else if (container.name == "hand")
        {
            updateHand(container);
        }
    }



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
            float y = hand.position.y + 0.001f * i;
            Vector3 p = new Vector3(x, y, hand.position.z);
            child.transform.position = p;

            i++;
        }

    }

    public int maxBoardSize;

    public void updateBoard(Transform board)
    {
        // Sort objects by their x position
        List<Transform> CardList = new List<Transform>();
        foreach (Transform card in board)
        {
            CardList.Add(card);    
        }


        CardList.Sort((p, q) => p.position.x.CompareTo(q.position.x));

       
        for (int n = 0; n < CardList.Count; n++)
        {
            CardList[n].SetSiblingIndex(n);
         //   Debug.Log($"{n}: Card List: {CardList[n].name}  \n Board: {board.GetChild(n).name}");

        }

        int cardsOnBoard = board.childCount;

        int i = 0;

        foreach (Transform child in board)
        {
            // set the position of the leftmost card equal to half the card's length
            float leftMostPosition = board.position.x - (margin / 2 * (cardsOnBoard - 1));
            // position every card after that one card's length to the right of eachother 
            float x = leftMostPosition + (margin * i);

            Vector3 p = new Vector3(x, board.localScale.y, board.position.z);
            child.transform.position = p;

            i++;
        }
    }
}



