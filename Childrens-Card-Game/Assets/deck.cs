using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck : MonoBehaviour
{
    public GameObject allCards;
    public GameObject hand1;
    public GameObject hand2;

    // Start is called before the first frame update
    void Start()
    {
      GameObject newCard = Instantiate(allCards.GetComponent<allCards>().card);
        //changes card's parent to the deck
        newCard.transform.parent = transform;

        draw(hand1);
    }


    public void draw(GameObject hand, int number = 1, int index = 1)
    {

        foreach (Transform child in transform)
        {
            int i = 1;
            if (i == index)
            {

                Transform drawnCard = Instantiate(child);
                drawnCard.transform.parent = hand.transform;
                Destroy(child.gameObject);
                
                
            }
            i++;
        }
    }
}
