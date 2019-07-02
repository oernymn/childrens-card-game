using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{


    bool clicked = false;

    float height = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (clicked == true)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 75f; // Set this to be the distance you want the object to be placed in front of the camera.
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


}
