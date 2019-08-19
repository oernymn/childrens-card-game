using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;
using static GetSet;

public class Attack : MonoBehaviour
{
    public Card attacker;
    public Card attacked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetWhatIsMousedOver() != null
            && GetWhatIsMousedOver().GetComponent<Card>() != null && GetWhatIsMousedOver().GetComponent<Stats>() != null)

            // Selects attacker
            if (GetWhatIsMousedOver().parent.name == "board1" && attacker == null)
            {
                attacker = GetWhatIsMousedOver().GetComponent<Card>();
                Debug.Log("Attacks at start: " + attacker.stats.attacks);

            }
            else if (attacker != null)
            {
                // If it's out of attacks it deselects.
                if (attacker.stats.attacks < 1)
                {
                    Debug.Log($"{attacker.transform.name} has no attacks left");
                    attacker = null;
                    return;
                }
                // Selects target.
                else
                {
                    attacker.stats.attacks -= 1;
                    attacked = GetWhatIsMousedOver().GetComponent<Card>();

                    List<Card> AfterList = new List<Card> { attacked, attacker };
                   
                    RunEffects(AfterList, Battle);
                    Debug.Log("Current health: " + attacked.stats.currentHealth);
                    Debug.Log("Attacks left: " + attacker.stats.attacks);
                    attacker = null;
                }

            }
    }

    private void Battle(List<Card> AfterList)
    {
        Debug.Log($"{attacker} attacks {attacked}!");
        attacker.status = Status.Attacking;
        attacked.status = Status.Defending;
        Debug.Log($"attacked health: {attacked.stats.currentHealth}. attacker damage: {attacked.stats.attack}!");

        attacked.GetComponent<Stats>().currentHealth -= attacker.GetComponent<Stats>().attack;
    }
}
