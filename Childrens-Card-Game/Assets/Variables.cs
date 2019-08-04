using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{

    static public Transform Hand { get
        {
            Functions.GetEnemyAllegiance().GetChild(handIndex);
        }
    }


    static public Update UpdateFunctions;

    static public Transform cam;

    static public Transform everything;
    static public Transform everythingBefore;
    static public Transform Alliance;
    static public Transform Horde;


    static public int board1Index = 0;
    static public int board2Index = 1;
    static public int handIndex = 2;
    static public int deckIndex = 3;
    static public int graveyardIndex = 4;

    static public Vector3 cardSize = new Vector3(0.063f, 0.002f, 0.088f);

    public enum Status
    {
        Neutral,
        Attacking,
        Defending,
        BeingBounced,
        BeingDrawn,
        BeingPlayed,
        BeingDamaged,
    }


    public enum CardType
    {
        Minion,
        Enchantment,
        Spell
    }


}
