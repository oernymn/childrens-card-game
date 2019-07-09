using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{


    public bool clicked = false;
   

    // Update is called once per frame
    void Update()
    {
        // Click and drag
        if (clicked == true)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 2f; // Set this to be the distance you want the object to be placed in front of the camera.
            transform.position = Camera.main.ScreenToWorldPoint(temp);
        }

    }


    void OnMouseDown()
    {
        // Set clicked status
        if (clicked == false)
        {
            clicked = true;


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
            
            transform.position += new Vector3(0, 1, 2);
        }
 
    }

    void OnMouseExit()
    {
        // put it back down after you stop hovering
        if (transform.parent.name == "hand1" || transform.parent.name == "hand2")
        {
            transform.position += new Vector3(0, -1, -2);
        }
    }

}