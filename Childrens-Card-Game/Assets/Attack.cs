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

        

        // When you click on a card on board1.
        if (Input.GetMouseButtonDown(0) && GetWhatIsMousedOver() != null && GetWhatIsMousedOver().GetComponent<Card>() != null
            && GetWhatIsMousedOver().parent.name == "board1")
        {

            Card minion = GetWhatIsMousedOver().GetComponent<Card>();

            if (minion.GetComponent<Card>().type == CardType.Minion && attacker == null)
            {
                attacker = minion;
                attacker.GetComponent<Card>().status = Status.ReadyToAttack;

                Debug.Log(attacker.name + " Ready to attack");

            }

           if (minion.GetComponent<Card>().type == CardType.Minion && minion != attacker)
            {
                Debug.Log($"{attacker} attacks {attacked}!");

                Card after = minion;
               

                RunEffects(after, attacker, Battle);


            }
        }
    }

    private void Battle(Card attacked, Card affacker)
    {
        attacked.currentHealth -= attacker.attack;
    }
}
