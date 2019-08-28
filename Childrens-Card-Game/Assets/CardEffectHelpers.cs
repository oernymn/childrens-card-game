using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;


public class CardEffectHelpers : MonoBehaviour
{
    // draw the Card at the indexes
    static public void TransferCard(Card caller, Transform targetContainer, Transform source, int[] indexes)
    {

        for (int i = 0; i < indexes.Length; i++)
        {

            // if the deck doesn't have enough Cards to contain the Card at indexes[i] -i then send error message.
            if (source.childCount < indexes[i] - i + 1)
            {

                Debug.Log("No Cards left");

            }
            else

            {
                // if the deck does have enough Cards: Identifies the Cards as they were before and after the effect.
                // -i to prevent index inflation from movement
                // Need to create a copy of everything because the assignment only creates a reference.

                List<Card> AfterList = new List<Card> { source.GetChild(indexes[i] - i).GetComponent<Card>(), caller, targetContainer.GetComponent<Card>() };

                RunEffects(AfterList, Move);

            }
        }

    }

    private static void Move(List<Card> AfterList)
    {
        Card card = AfterList[0];
        Card caller = AfterList[1];
        Transform targetContainer = AfterList[2].transform;

        GetSet.SetTarget(caller, card);
        card.transform.parent = targetContainer;
    }

  
}
