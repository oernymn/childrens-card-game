using System;
using System.Collections.Generic;
using UnityEngine;
using static GetSet;

public class EffectTargeting : MonoBehaviour
{
    public static bool targeting = false;
    static List<Card> AffectedList;
    public static Action<List<Card>> CardEffect { get; private set; }

    public Transform RedDot;

    public static void SetInfo(Card card, Action<List<Card>> Effect)
    {
        targeting = true;
        AffectedList = new List<Card> { card };
        CardEffect = Effect;
    }


    // Update is called once per frame
    void Update()
    {
        if (targeting == true)
        {
            // Makes the red dot follow the cursor.
            Vector3 temp = Input.mousePosition;
            temp.z = 0.4f; // Set this to be the distance you want the object to be placed in front of the camera.
            RedDot.position = Camera.main.ScreenToWorldPoint(temp);

            if (Input.GetMouseButtonDown(0) == true)
            {

                if (GetWhatIsMousedOver() != null && GetWhatIsMousedOver().GetComponent<Card>() != null)
                {
                    AffectedList.Add(GetWhatIsMousedOver().GetComponent<Card>());
                    Functions.RunEffects(AffectedList, CardEffect);
                    targeting = false;
                    RedDot.position = new Vector3(69, 69, 69);
                }
            }

        }



    }

    

}
