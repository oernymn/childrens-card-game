using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static cardEffectFunctions;

public class Movement : MonoBehaviour
{

    public Transform cam;


    public Transform cardsFunctionsEtc;
    protected cardEffectFunctions Functions;

    Transform board1;
    Transform board2;
    Transform hand;
    Transform deck;

    private void Awake()
    {
        cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
        Functions = cardsFunctionsEtc.GetComponent<cardEffectFunctions>();

        cam = Functions.cam;

        hand = transform.parent.parent.GetChild(Functions.handIndex);
        board1 = transform.parent.parent.GetChild(Functions.board1Index);
        board2 = transform.parent.parent.GetChild(Functions.board2Index);
        deck = transform.parent.parent.GetChild(Functions.deckIndex);

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
            transform.localScale = Functions.cardSize;
        }
    }

    // Playing cards
    private void OnMouseUp()
    {
        // Ignore raycast. Otherwise the card will block the raycast.
        transform.gameObject.layer = 2;

        
        Transform droppedOn;

        Transform board1 = transform.parent.parent.GetChild(Functions.board1Index);
        Transform board2 = transform.parent.parent.GetChild(Functions.board2Index);
        Transform hand = transform.parent.parent.GetChild(Functions.handIndex);


        // Need to play cards from hand.
        if (transform.parent != hand)
        {
            Debug.Log("Trying to play a card that's not in you hand.");
            transform.gameObject.layer = 0;
            return;
        }


        // Defines the thing it's dropped on.
        if (Functions.GetWhatIsMousedOver() != null)
        {
            droppedOn = Functions.GetWhatIsMousedOver();
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

                    // This card's target is the matched card.
                    after.GetComponent<Card>().target = droppedOn;
                    droppedOn.GetComponent<Card>().targeter = after;

                    before = Functions.RunEffects(before, after);

                    after.GetComponent<Card>().status = Status.Neutral;
                    after.parent = transform.parent.parent.GetChild(Functions.graveyardIndex);

                    before = Functions.RunEffects(before, after);
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

    public void PlayToBoard(Transform card, Transform targetBoard)
    {
        // Snaps back if the board is full.
        if (targetBoard.childCount >= cardsFunctionsEtc.GetComponent<Update>().maxBoardSize)
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


    void OnMouseEnter()
    {
        // zoom in on mouse over in hand

        // If it's in the right position  in the hand
        if (transform.parent == hand && transform.position.z == hand.position.z)
        {
            // Increase the card's size and sets the z position so it fits the screen 

            transform.localScale = Functions.cardSize * 2;

            transform.localPosition += new Vector3(0, 0, 0.06f);

            //Debug.Log(transform.localScale.z);
        }
    }

    void OnMouseExit()
    {
        // put it back down after you stop hovering
        if (transform.parent == hand)
        {
            transform.localScale = Functions.cardSize; ;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.parent.position.z);
        }
    }





}

