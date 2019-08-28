using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int baseHealth;
    public int maxHealth;
    public int currentHealth;
    public int baseAttack;
    public int attack;

    public int attacks;



    public void SetStats (int atk, int hp)
    {
        currentHealth = maxHealth = hp;
        attack = atk;
    }

}
