using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static GetSet;
using static Functions;

public class AttackTargeting : MonoBehaviour
{
    public Transform RedDot;
    bool attacking;

    private void OnMouseDrag()
    {
        // If it's on board1 and can attack.
        if (transform.parent == GetContainer(GetComponent<Card>(), true, board1Index)
            && GetComponent<Stats>() != null && GetComponent<Stats>().attacks > 0) {

            Vector3 temp = Input.mousePosition;

            temp.z = 0.4f ; // Set this to be the distance you want the object to be placed in front of the camera.
            RedDot.position = Camera.main.ScreenToWorldPoint(temp);

            attacking = true;
        }
        else if (GetComponent<Stats>().attacks <= 0)
        {
            Debug.Log("Not enough attacks");
        }
    }

    private void OnMouseUp()
    {
        if (attacking == true)
        {
            // Ignore raycast. Otherwise the red sot will block the raycast.
            RedDot.gameObject.layer = 2;

            Card attacker = GetComponent<Card>();
            Card attacked = GetWhatIsMousedOver().GetComponent<Card>();

            RedDot.position = new Vector3 (69, 69, 69);

            attacker.stats.attacks -= 1;

            List<Card> AfterList = new List<Card> { attacker, attacked };

            RunEffects(AfterList, Battle);

            attacker.GetComponent<Movement>().AttackAnimation(attacked);


            Debug.Log("Current health: " + attacked.stats.currentHealth);
            Debug.Log("Attacks left: " + attacker.stats.attacks);

            attacking = false;
            RedDot.gameObject.layer = 0;

        }

    }
    private void Battle(List<Card> AfterList)
    {
        Debug.Log($"{AfterList[0]} attacks {AfterList[1]}!");
        AfterList[0].status = Status.Attacking;
        AfterList[1].status = Status.Defending;
        Debug.Log($"attacked health: {AfterList[1].stats.currentHealth}. attacker damage: {AfterList[0].stats.attack}");

        AfterList[1].GetComponent<Stats>().currentHealth -= AfterList[0].GetComponent<Stats>().attack;
    }
}




