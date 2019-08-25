using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;

public class Targeting : MonoBehaviour
{
    public Transform RedDot;

    private void OnMouseDrag()
    {
        // If it's 
        if (transform.parent == GetSet.GetContainer(GetComponent<Card>(), true, Variables.board1Index)) {

            Vector3 temp = Input.mousePosition;

            temp.z = cam.position.y - transform.position.y; // Set this to be the distance you want the object to be placed in front of the camera.
            RedDot.position = Camera.main.ScreenToWorldPoint(temp);

            
        }
    }
}
