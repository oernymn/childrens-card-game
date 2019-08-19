using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;

public class GetSet : MonoBehaviour
{
    static public Transform GetContainer(Card card, bool isEnemy, int ContainerIndex)
    {
        Transform container;

        if (card.transform.parent.parent == Alliance)
        {
            if (isEnemy)
            {
                container = Horde.GetChild(ContainerIndex);
            }
            else
            {
                container = Alliance.GetChild(ContainerIndex);
            }
        }
        else
        {
            if (isEnemy)
            {
                container = Alliance.GetChild(ContainerIndex);
            }
            else
            {
                container = Horde.GetChild(ContainerIndex);
            }
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
