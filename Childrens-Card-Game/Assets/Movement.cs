using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    



    public Transform cam;

    public Transform board1;
    public Transform board2;
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
        board2 = Functions.board2;
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
        if(transform.position.z < -2)
        {
            Functions.updateAll();
        } else

        // If it is within the boundries of the board and it's a minion it plays it.
        if (transform.position.x > -3.75f && transform.position.x < 3.75f
            && transform.position.z > -2 && transform.position.z < 0 
            && this.GetComponent<Card>().type == Card.Type.Minion)
        {
            // Snaps back if the board is full.
            if (board1.childCount >= cardsFunctionsEtc.GetComponent<Update>().maxBoardSize)
            {
                Functions.updateAll();
                return;
            }

            Transform after = transform;
            Transform before = Functions.SetBefore(after);

            after.GetComponent<Card>().status = Card.Status.BeingPlayed;
            // Runs any OnPlay effects.
            before = Functions.RunEffects(before, after, false);

            after.transform.parent = board1;
            // Checks OnSummon effects.
            Functions.RunEffects(before, after);


        }
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

