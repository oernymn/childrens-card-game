using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{

    static public Update UpdateFunctions;

    static public Transform cam;


    static public Transform everything;
    static public Transform everythingBefore;
    static public Transform Alliance;

    static public Transform Horde;
    static public Transform RedDot;

    static public int heroIndex = 0;
    static public int board1Index = 1;
    static public int board2Index = 2;
    static public int handIndex = 3;
    static public int deckIndex = 4;
    static public int graveyardIndex = 5;

    static public Vector3 cardSize = new Vector3(0.063f, 0.002f, 0.088f);

    static public List<Card> ChangedAffectedList;


    public enum KeyWord
    {
        Taunt
    }

    public enum Status
    {
        Neutral,
        ReadyToAttack,
        Attacking,
        Defending,
        BeingBounced,
        BeingDrawn,
        BeingPlayed,
        BeingDamaged,
    }

    public enum CardType
    {
        Spell,
        Minion,
        Support,
        Hero,
        Container

    }

    public enum Tribe
    {
        Beast,
        Dragon

    }

    private void Awake()
    {
        cam = GameObject.Find("Main Camera").transform;
        everything = GameObject.Find("everything").transform;
        everythingBefore = GameObject.Find("everythingBefore").transform;
        Alliance = GameObject.Find("Alliance").transform;
        Horde = GameObject.Find("Horde").transform;
        UpdateFunctions = GetComponent<Update>();
        RedDot = GameObject.Find("RedDot").transform;



    }

}
