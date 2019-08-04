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

        Transform board1 = transform.parent.parent.GetChild(board1Index);
        Transform board2 = transform.parent.parent.GetChild(board2Index);
        Transform hand = transform.parent.parent.GetChild(handIndex);


        // Need to play cards from hand.
        if (transform.parent != hand)
        {
            Debug.Log("Trying to play a card that's not in you hand.");
            transform.gameObject.layer = 0;
            return;
        }


        // Defines the thing it's dropped on.
        if (GetWhatIsMousedOver() != null)
        {
            droppedOn = GetWhatIsMousedOver();
        }
        else
        {
            return;
        }


        Debug.Log($" Dropped on: {droppedOn}.");

        // If it's a targeting spell.
        if (GetComponent<Card>().type == CardType.Spell && GetComponent<Card>().GetSelection() != null)
        {

            List<Transform> cardSelectionList = GetComponent<Card>().GetSelection();

            foreach (Transform card in cardSelectionList)
            {
                // If the card the spell dropped on matches a card that the card can target.
                if (card == droppedOn)
                {

                    Debug.Log($"Spell cast on {droppedOn}");


                    Transform after = transform;
                    Transform before = Functions.SetBefore(after);

                    after.GetComponent<Card>().status = Status.BeingPlayed;
                    after.GetComponent<Card>().target = droppedOn;

                    RunEffects(before, after);

                    after.GetComponent<Card>().status = Status.Neutral;
                    after.parent = transform.parent.parent.GetChild(graveyardIndex);


                }
            }

        }



        // If it is within the boundries of boardX and it's a minion it plays it.
        else if (droppedOn == board1 && GetComponent<Card>().type == CardType.Minion)
        {
            PlayToBoard(transform, board1);
        }

        // if it is within boardXX and it's an enchantment play it.
        else if (droppedOn == board2 && GetComponent<Card>().type == CardType.Enchantment)
        {
            PlayToBoard(transform, board2);
        }


        // Sets layer back to default.

        transform.gameObject.layer = 0;
        Functions.updateAll();
    }

    public void PlayCard(Transform after, Transform target)
    {

    }

    public void PlayToBoard(Transform card, Transform targetBoard)
    {
        // Snaps back if the board is full.
        if (targetBoard.childCount >= GetComponent<Update>().maxBoardSize)
        {
            Functions.updateAll();
            return;
        }

        Transform after = transform;
        Transform before = Functions.SetBefore(after);

        after.GetComponent<Card>().status = Status.BeingPlayed;
        // Runs any OnPlay effects. Not updating because then it would update the hand and screw up the x positions

        Debug.Log($"Before: {before.name}. After: {after.name}");
        before = Functions.RunEffects(before, after, false);

        after.transform.parent = targetBoard;
        // Checks OnSummon effects.
        before = Functions.RunEffects(before, after);
    }

    public Transform GetWhatIsMousedOver()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            return hit.transform;

        }
        else
        {
            Functions.updateAll();
            return null;
        }
    }
}
