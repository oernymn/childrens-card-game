using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GetSet;

public class EffectTargeting : MonoBehaviour
{
    public static bool targeting = false;

    public Transform RedDot;

    // Update is called once per frame
    void Update()
    {
        if (targeting == true)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 0.4f; // Set this to be the distance you want the object to be placed in front of the camera.
            RedDot.position = Camera.main.ScreenToWorldPoint(temp);

            if (Input.GetMouseButtonDown(0) == true)
            {
                RedDot.gameObject.layer = 2;

                if (GetWhatIsMousedOver() != null && GetWhatIsMousedOver().GetComponent<Card>() != null)
                {

                }
            }

        }



    }

    

}
