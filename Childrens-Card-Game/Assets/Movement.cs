using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{


    bool clicked = false;

    float height = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (clicked == true)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 50f; // Set this to be the distance you want the object to be placed in front of the camera.
            transform.position = Camera.main.ScreenToWorldPoint(temp);
        }
        Debug.Log(transform.position);
    }


    void OnMouseDown()
    {
        if (clicked == false)
        {
            clicked = true;
        } else
        {
            clicked = false;
        }
    }

    void OnMouseEnter()
    {
        transform.position += Vector3.up * 10;
        transform.position += Vector3.forward * 10;
    }
    
    void OnMouseExit()
    {
        transform.position += Vector3.down * 10;
        transform.position += Vector3.back * 10;

    }

}
