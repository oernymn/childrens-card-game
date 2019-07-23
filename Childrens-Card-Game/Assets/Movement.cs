using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{


    private bool hovered = false;

    public Transform board1;
    public Transform board2;
    public Transform cardsFunctionsEtc;

    protected cardEffectFunctions Functions;

    private void Awake()
    {
        board1 = GameObject.Find("board1").transform;
        board2 = GameObject.Find("board2").transform;
        cardsFunctionsEtc = GameObject.Find("cardsFunctionsEtc").transform;
        Functions = cardsFunctionsEtc.GetComponent<cardEffectFunctions>();

    }




    private void OnMouseDrag()
    {
        // Click and drag
        Vector3 temp = Input.mousePosition;
        temp.z = 5 - transform.position.y; // Set this to be the distance you want the object to be placed in front of the camera.
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

    /*


            // if it is within the boundries of the board 
            if (transform.position.x > -3.75f && transform.position.x < 3.75f
                     && transform.position.z > -2 && transform.position.z < 0)
            {}

    */


    private void OnMouseDown()
    {
        if (hovered == true)
        {
            transform.localScale = new Vector3(0.063f, 0.002f, 0.088f);
        }
    }

    private void OnMouseUp()
    {
        if(transform.position.z < -2)
        {
            Functions.updateAll();
        }
    }

    void OnMouseEnter()
    {
        // zoom in on mouse over in hand
        hovered = true;

        // If it's in the right position  in the hand
        if ( transform.position.z == -2.5)
        {
            // Increase the card's size and sets the z position so it fits the screen 

            transform.localScale = new Vector3(0.063f * 2, 0.002f * 2, 0.088f * 2);
            // 4 somehow works
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
            //Debug.Log(transform.localScale.z);
        }
    }

    void OnMouseExit()
    {
        hovered = false;
        // put it back down after you stop hovering
        if (transform.position.z == -2)
        {
            transform.localScale = new Vector3(0.063f, 0.002f, 0.088f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
        }
    }


  


}

