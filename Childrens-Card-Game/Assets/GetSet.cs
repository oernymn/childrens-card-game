using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;


public class GetSet : MonoBehaviour
{

    static public void SetContainer(Card card, bool isAlly, int containerIndex)
    {
        // Wether it's a Becoming card or a real card the container must always be set.
        card.Container = GetContainer(card, isAlly, containerIndex);

        // If it's a real card it changes parent, if you would change the parent of a Becoming card it would jump out of everythingBefore and you'd have more cards. 
        if (card.transform.parent.parent == Horde || card.transform.parent.parent == Alliance)
        {
            card.transform.parent = card.Container;
        }
    }

    static public Transform GetContainer(Card card, bool isAlly, int ContainerIndex)
    {
        Transform container;

        if (card.Allegiance == Alliance)
        {
            if (isAlly)
            {
                container = Alliance.GetChild(ContainerIndex);
            }
            else
            {
                container = Horde.GetChild(ContainerIndex);
            }
        }
        else if (card.Allegiance == Horde)
        {
            if (isAlly)
            {
                container = Horde.GetChild(ContainerIndex);
            }
            else
            {
                container = Alliance.GetChild(ContainerIndex);
            }
        } else
        {
            Debug.Log(card.Allegiance);
            return null;
        }
        return container;
    }



    static public Transform GetWhatIsMousedOver()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {

            return hit.transform;

        }
        else
        {
            return null;
        }
    }

    static public void SetTarget(Card targeter, Card target)
    {
        targeter.target = target;
        target.targeter = targeter;

    }

    public static List<Card> ReturnToNeutral(List<Card> AfterList)
    {
        foreach (Card card in AfterList)
        {
            card.status = Status.Neutral;
            card.target = card.targeter =
            card.affects = card.affecter =
            null;
        }

        return AfterList;
    }
}
