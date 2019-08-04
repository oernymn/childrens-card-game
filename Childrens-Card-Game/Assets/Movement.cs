using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;

public class Movement : MonoBehaviour
{

//    public Transform cardsFunctionsEtc;
 //   protected Functions Functions;

    Transform board1;
    Transform board2;
    Transform hand;
    Transform deck;

    private void Awake()
    {
      //   cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
       //  Functions = cardsFunctionsEtc.GetComponent<Functions>();

       

        hand = transform.parent.parent.GetChild(handIndex);
        board1 = transform.parent.parent.GetChild(board1Index);
        board2 = transform.parent.parent.GetChild(board2Index);
        deck = transform.parent.parent.GetChild(deckIndex);

    }

    private void OnMouseDrag()
    {
        // Click and drag
        if (transform.parent == hand)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = cam.position.y - transform.position.y; // Set this to be the distance you want the object to be placed in front of the camera.
            transform.position = Camera.main.ScreenToWorldPoint(temp);
        }
    }


    // Shrinks the card when you try to play it. 
    private void OnMouseDown()
    {
        if (transform.parent == hand)
        {
            transform.localScale = cardSize;
        }
    }



    void OnMouseEnter()
    {
        // zoom in on mouse over in hand

        // If it's in the right position  in the hand
        if (transform.parent == hand && transform.position.z == hand.position.z)
        {
            // Increase the card's size and sets the z position so it fits the screen 

            transform.localScale = cardSize * 2;

            transform.localPosition += new Vector3(0, 0, 0.06f);

            //Debug.Log(transform.localScale.z);
        }
    }

    void OnMouseExit()
    {
        // put it back down after you stop hovering
        if (transform.parent == hand)
        {
            transform.localScale = cardSize; ;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.parent.position.z);
        }
    }





}

