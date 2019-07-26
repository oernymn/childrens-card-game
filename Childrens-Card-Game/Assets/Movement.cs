using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    



    public Transform cam;

    public Transform board1;
    public Transform board12;

    public Transform board2;
    public Transform board22;

    public Transform hand1;
    public Transform hand2;
    public Transform cardsFunctionsEtc;

    protected cardEffectFunctions Functions;

    private void Awake()
    {
        cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
        Functions = cardsFunctionsEtc.GetComponent<cardEffectFunctions>();

        cam = Functions.cam;

        board1 = Functions.board1;
        board12 = Functions.board12;

        board2 = Functions.board2;
        board22 = Functions.board22;

        hand1 = Functions.hand1;
        hand2 = Functions.hand2;
    }




    private void OnMouseDrag()
    {
        // Click and drag
        Vector3 temp = Input.mousePosition;
        temp.z = cam.position.y - transform.position.y; // Set this to be the distance you want the object to be placed in front of the camera.
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

    private void OnMouseDown()
    {
            transform.localScale = new Vector3(0.063f, 0.002f, 0.088f);
    }

    // Playing cards
    private void OnMouseUp()
    {
        // Snaps cards back to the hand if it isn't played.
        Transform boardX;
        Transform boardXX;
        Transform targetBoard;
     

          if ( transform.parent == hand1)
        {
            boardX = board1;
            boardXX = board12;
        } else if (transform.parent == hand2 ) {
            boardX = board2;
            boardXX = board22;
        } else
        {
            Functions.updateAll();
            return;
        }

        // If it is within the boundries of the board and it's a minion it plays it.
        if (transform.position.x > boardX.position.x - boardX.localScale.x / 2
            && transform.position.x < boardX.position.x + boardX.localScale.x / 2

            && transform.position.z > boardX.position.z - boardX.localScale.z / 2
            && transform.position.z < boardX.position.z + boardX.localScale.z / 2
            && this.GetComponent<Card>().type == Card.Type.Minion)
        {

            targetBoard = boardX;

        } else if (transform.position.x > boardXX.position.x - boardXX.localScale.x / 2
            && transform.position.x < boardXX.position.x + boardXX.localScale.x / 2

            && transform.position.z > boardXX.position.z - boardXX.localScale.z / 2
            && transform.position.z < boardXX.position.z + boardXX.localScale.z / 2
            && this.GetComponent<Card>().type == Card.Type.Minion)
        {

            targetBoard = boardXX;
     
        } else
        {
            Functions.updateAll();
            return;
        }

        // Snaps back if the board is full.

        if (targetBoard.childCount >= cardsFunctionsEtc.GetComponent<Update>().maxBoardSize)
            {
                Functions.updateAll();
                return;
            }

            Transform after = transform;
            Transform before = Functions.SetBefore(after);

            after.GetComponent<Card>().status = Card.Status.BeingPlayed;
            // Runs any OnPlay effects. Not updating because then it would update
            before = Functions.RunEffects(before, after, false);

            after.transform.parent = targetBoard;
            // Checks OnSummon effects.
            Functions.RunEffects(before, after);
 
    }

    void OnMouseEnter()
    {
        // zoom in on mouse over in hand
       
        // If it's in the right position  in the hand
        if ((transform.parent == hand1 && transform.position.z == hand1.position.z)
            || (transform.parent == hand2 && transform.position.z == hand2.position.z))
        {
            // Increase the card's size and sets the z position so it fits the screen 

            transform.localScale = new Vector3(0.063f * 2, 0.002f * 2, 0.088f * 2);
         
           transform.localPosition += new Vector3(0, 0, 0.06f);

            //Debug.Log(transform.localScale.z);
        }
    }

    void OnMouseExit()
    {
        // put it back down after you stop hovering
        if ((transform.parent == hand1 )
            || (transform.parent == hand2))
        {
            transform.localScale = new Vector3(0.063f, 0.002f, 0.088f);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.parent.position.z);
        }
    }


  


}

