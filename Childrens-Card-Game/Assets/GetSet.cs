using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;


public class GetSet : MonoBehaviour
{

    static public List<Card> ListCardsInContainer (Transform Container)
    {
        List<Card> List = new List<Card>();

        foreach (Transform child in Container)
        {
            List.Add(child.GetComponent<Card>());
        }

        return List;
    }

    static public void SetContainer(Card card, bool isAlly, int containerIndex)
    {
        // Wether it's a Becoming card or a real card the container must always be set.
        card.Container = GetContainer(card, isAlly, containerIndex);

        // If it's a real card it changes parent, if you would change the parent of a Becoming card it would jump out of everythingBefore and you'd have more cards. 
        if (card.transform.parent != everythingBefore)
        {
            card.transform.parent = card.Container;
        }
    }

    static public void SetContainer(Card card, Transform Container)
    {
        // Wether it's a Becoming card or a real card the container must always be set.
        card.Container = Container;

        // If it's a real card it changes parent, if you would change the parent of a Becoming card it would jump out of everythingBefore and you'd have more cards. 
        if (card.transform.parent != everythingBefore)
        {
            card.transform.parent = Container;
        }
    }

    static public Transform GetContainer(Card card, bool isAlly, int ContainerIndex)
    {
        Transform container;

            if (isAlly)
            {
                container = card.Allegiance.GetChild(ContainerIndex);
            }
            else if (card.Allegiance == Alliance)
            {
                container = Horde.GetChild(ContainerIndex);
            } else
        {
            container = Alliance.GetChild(ContainerIndex);
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
