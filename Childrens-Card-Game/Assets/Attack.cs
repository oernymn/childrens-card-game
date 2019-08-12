using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;
using static Functions;

public class Attack : MonoBehaviour
{
    public Card attacker;
    public Card attacked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetWhatIsMousedOver() != null)

        // Selects attacker
        if (GetWhatIsMousedOver().GetComponent<Card>().type == CardType.Minion
            && (GetWhatIsMousedOver().parent.name == "board1" && attacker == null))
        {
            attacker = GetWhatIsMousedOver().GetComponent<Card>();
           

        } else if (attacker != null)
            {
                // If it's out of attacks it deselects.
                if (attacker.GetComponent<Stats>().attacks < 1)
                {
                    attacker = null;
                }
                // Selects target.
                else
                {
                    attacked = GetWhatIsMousedOver().GetComponent<Card>();
                    RunEffects(attacked, attacker, Battle);
                }


            }
    }

    private void Battle(Card attacked, Card attacker)
    {
        Debug.Log($"{attacker} attacks {attacked}!");
        attacker.status = Status.Attacking;
        attacked.status = Status.Defending;
        attacked.GetComponent<Stats>().currentHealth -= attacker.GetComponent<Stats>().attack;
    }
}
