using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;





    public class Collections : MonoBehaviour
{

    public Transform AllCardsPrefab;


    public static Dictionary<string, Card> AllCards = new Dictionary<string, Card>();
    public static Dictionary<string, Card> Minions = new Dictionary<string, Card>();
    public static Dictionary<string, Card> Spells = new Dictionary<string, Card>();
    public static Dictionary<string, Card> Supports = new Dictionary<string, Card>();

    private  void Start()
    {
        foreach (Transform child in AllCardsPrefab)
        {
            Card card = child.GetComponent<Card>();

            AllCards.Add(card.name, card);
            Debug.Log($"{card.name} added to AllCardsList");

            if (card.type == CardType.Minion)
            {
                Minions.Add(card.name, card);
                Debug.Log($"{card.name} added to Minions");

            }

            if (card.type == CardType.Spell)
            {
                Spells.Add(card.name, card);
                Debug.Log($"{card.name} added to Spells");
            }

        }
    }
}
