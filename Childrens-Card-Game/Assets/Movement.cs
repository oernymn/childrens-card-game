using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
  

    private bool clicked = false;

    public Transform board1;

    private void Awake()
    {
      board1 = GameObject.Find("board1").transform;
}


// Update is called once per frame
void Update()
    {
        // Click and drag
        if (clicked == true)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 5 - transform.position.y; // Set this to be the distance you want the object to be placed in front of the camera.
            transform.position = Camera.main.ScreenToWorldPoint(temp);
        }

        // if it is within the boundries of the board
        if (transform.position.x > -3.75f && transform.position.x < 3.75f
                 && transform.position.z > -2 && transform.position.z < 0)
        {
            Debug.Log("parent should be board1");
            transform.SetParent(board1);
            Debug.Log(transform.parent);
        }

    }


    void OnMouseDown()
    {
        // Set clicked status
        if (clicked == false)
        {
            clicked = true;
            // makes it small again after being hovered 
            transform.position -= new Vector3(0, 0, transform.localScale.z * 4);
            transform.localScale /= 2;
        }
        else
        {
            clicked = false;
            
            
        }

       

    }

    void OnMouseEnter()
    {
        // zoom in on mouse over in hand
        if (transform.parent.name == "hand1" || transform.parent.name == "hand2")

        {
            // 4 somehow works
            transform.localScale *= 2;
            transform.position += new Vector3(0, 0, transform.localScale.z * 4); 
            //Debug.Log(transform.localScale.z);

        }

    }

    void OnMouseExit()
    {
        // put it back down after you stop hovering
        if (transform.parent.name == "hand1" || transform.parent.name == "hand2")
        {
            
            transform.position -= new Vector3(0, 0, transform.localScale.z * 4);
            transform.localScale /= 2;
        }
    }

}