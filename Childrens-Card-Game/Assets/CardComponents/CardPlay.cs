﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;
using static GetSet;

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

        transform.gameObject.layer = 0;


        Debug.Log($" Dropped on: {droppedOn}.");

        // If it's a targeting spell.

        if (GetComponent<Card>().type == CardType.Spell && GetComponent<Card>().GetEffectTargets() != null)
        {
            Debug.Log($" Targeting");

            List<Card> SelectionList = GetComponent<Card>().GetEffectTargets();

                // If the card the spell dropped on matches a card that the card can target.
                if (SelectionList.Contains(droppedOnCard))
                {
                    Debug.Log($"Spell cast on {droppedOnCard}");

                    List<Card> AfterList = RunEffects(new List<Card> { GetComponent<Card>(), droppedOnCard }, PlayCardTargeted);
                    AfterList = RunEffects(AfterList, SendToGraveyard);

                }
            

        }

        else if (GetComponent<Card>().type == CardType.Spell)
        {
            Debug.Log(GetComponent<Card>().name + " played without targets");
            List<Card> AffectedList = RunEffects(new List<Card> { GetComponent<Card>() }, PlayCard);
        }

        // If it is within the boundries of boardX and it's a minion it plays it.
        else if (droppedOn == board1 && GetComponent<Card>().type == CardType.Minion)
        {
            RunEffects(new List<Card> { GetComponent<Card>(), board1.GetComponent<Card>() }, PlayToBoard);
        }

        // if it is within boardXX and it's an enchantment play it.
        else if (droppedOn == board2 && GetComponent<Card>().type == CardType.Support)
        {
            RunEffects(new List<Card> { GetComponent<Card>(), board2.GetComponent<Card>() }, PlayToBoard);
        }


        // Sets layer back to default.

        updateAll();
    }

    private static void PlayCard(List<Card> AfterList)
    {

        AfterList[0].status = Status.BeingPlayed;
    }

    private static void PlayCardTargeted(List<Card> AfterList)
    {
        AfterList[0].status = Status.BeingPlayed;
        SetTarget(AfterList[0], AfterList[1]);
        Debug.Log($"Target: {AfterList[0].target}");
    }

    private void SendToGraveyard(List<Card> AfterList)
    {
        SetContainer(AfterList[0], true, graveyardIndex);
    }

    private void PlayToBoard(List<Card> AfterList)
    {

        Debug.Log(AfterList[0].name);

        // Snaps back if the board is full.
        if (AfterList[1].transform.childCount >= UpdateFunctions.maxBoardSize)
        {
            updateAll();
            return;
        }

        AfterList[0].status = Status.BeingPlayed;
        SetContainer(AfterList[0], AfterList[1].transform);
        

        // Runs any OnPlay effects. Not updating because then it would update the hand and screw up the x positions
    }



}
