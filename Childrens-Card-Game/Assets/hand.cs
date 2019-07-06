using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class hand : MonoBehaviour
{
    
    public int maxHandSize;

    // puts the cards in the right position
    public void updateHand()
    {
        Transform cardExample = transform.GetChild(0);
        float length = cardExample.localScale.x;
        int cardsInHand = transform.childCount;
        int i = 0;

        foreach (Transform child in transform)
        {
            // set the position of the leftmost card
            float leftMostPosition = transform.position.x - (length / 2 * (cardsInHand - 1));
            // position every card after that a card's length to the right of eachother 
            float x = leftMostPosition + (length * i);
            // stack them on top of eachother
            float y = 0.1f * i;
            Vector3 p = new Vector3(x, y, transform.position.z);
            child.transform.position = p;
            //child.transform.position = Vector3.MoveTowards(transform.position, p, 1);

            i++;
        }
    }

    private void Start()
    {
        updateHand();

    }
    // Update is called once per frame
    void Update()
    {



    }
}
