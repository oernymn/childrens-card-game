using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;

public class CardPlay : MonoBehaviour
{
    // Playing cards
    private void OnMouseUp()
    {
        // Ignore raycast. Otherwise the card will block the raycast.
        transform.gameObject.layer = 2;


        Transform droppedOn;
        Card droppedOnCard;
        Transform board1 = transform.parent.parent.GetChild(board1Index);
        Transform board2 = transform.parent.parent.GetChild(board2Index);
        Transform hand = transform.parent.parent.GetChild(handIndex);


        // Need to play cards from hand.
        if (transform.parent != hand)
        {
            transform.gameObject.layer = 0;
            return;
        }


        // Defines the thing it's dropped on.
        if (GetWhatIsMousedOver() != null)
        {

            droppedOn = GetWhatIsMousedOver();
            droppedOnCard = GetWhatIsMousedOver().GetComponent<Card>();

        }
        else
        {
            Debug.Log($"Drop Fail.");
            transform.gameObject.layer = 0;
            updateAll();
            return;
        }


        Debug.Log($" Dropped on: {droppedOn}.");

        // If it's a targeting spell.
        if (GetComponent<Card>().type == CardType.Spell && GetComponent<Card>().GetTargets() != null)
        {

            List<Card> SelectionList = GetComponent<Card>().GetTargets();


            foreach (Card card in SelectionList)
            {
                // If the card the spell dropped on matches a card that the card can target.
                if (card == droppedOnCard)
                {

                    Debug.Log($"Spell cast on {droppedOnCard}");


                    List<Card> AfterList = new List<Card> { GetComponent<Card>(), droppedOnCard };
                    RunEffects(AfterList, PlayCardTargeted);

                }
            }

        }
        // If it is within the boundries of boardX and it's a minion it plays it.
        else if (droppedOn == board1 && GetComponent<Card>().type == CardType.Minion)
        {
            transform.gameObject.layer = 0;

            PlayToBoard(transform, board1);
        }

        // if it is within boardXX and it's an enchantment play it.
        else if (droppedOn == board2 && GetComponent<Card>().type == CardType.Support)
        {
            transform.gameObject.layer = 0;

            PlayToBoard(transform, board2);
        }


        // Sets layer back to default.

        transform.gameObject.layer = 0;
        updateAll();
    }

    private static void PlayCardTargeted(List<Card> AfterList)
    {
        Debug.Log("It's Played...");
        AfterList[0].status = Status.BeingPlayed;
        AfterList[0].target = AfterList[1];
    }

    private void SendToGraveyard(Card after)
    {
        after.status = Status.Neutral;
        Debug.Log(after.transform.parent);
        Debug.Log(after.transform.parent.parent);
        Debug.Log(graveyardIndex);
        Debug.Log(after.transform.parent.parent.GetChild(graveyardIndex));

        after.transform.parent = after.transform.parent.parent.GetChild(graveyardIndex);
    }

   


    public void PlayToBoard(Transform card, Transform targetBoard)
    {
        // Snaps back if the board is full.
        if (targetBoard.childCount >= UpdateFunctions.maxBoardSize)
        {
            Functions.updateAll();
            return;
        }

        List<Card> AfterList = new List<Card> { GetComponent<Card>(), targetBoard.GetComponent<Card>()};

        RunEffects(AfterList, PutOnBoard);

        // Runs any OnPlay effects. Not updating because then it would update the hand and screw up the x positions
    }

    private static void PutOnBoard(List<Card> AfterList)
    {
        AfterList[0].status = Status.BeingPlayed;
        AfterList[0].transform.parent = AfterList[1].transform;
    }


}
