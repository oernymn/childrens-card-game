

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;
using static GetSet;

public  class Card : MonoBehaviour
{

    // For the 'before' cards.
    public Transform Allegiance;
    public Transform Container;
    public int Index;

    public Card hero { get { return Allegiance.GetChild(heroIndex).GetComponent<Card>(); } }
    public Card enemyHero { get { return GetContainer(this, false, heroIndex).GetComponent<Card>(); } }

    public Transform board1 { get { return Allegiance.GetChild(board1Index); } }
    public Transform enemyBoard1 { get { return GetContainer(this, false, board1Index); } }

    public Transform board2 { get { return Allegiance.GetChild(board2Index); } }
    public Transform enemyBoard2 { get { return GetContainer(this, false, board2Index); } }

    public Transform hand { get { return Allegiance.GetChild(handIndex); } }
    public Transform enemyHand { get { return GetContainer(this, false, handIndex); } }

    public Transform deck { get { return Allegiance.GetChild(deckIndex); } }
    public Transform enemyDeck { get { return GetContainer(this, false, deckIndex); } }

    public Transform graveyard { get { return Allegiance.GetChild(graveyardIndex); } }
    public Transform enemyGraveyard { get { return GetContainer(this, false, graveyardIndex); } }

    public Status status;
    public CardType type;
    public HashSet<Tribe> Tribes;
    public HashSet<KeyWord> KeyWords = new HashSet<KeyWord>();

    public Stats stats;

    public Card target;
    public Card targeter;
    public Card affects;
    public Card affecter;

    public virtual void AfterCardEffect(object sender, EffectEventArgs e) { }
    public virtual void WheneverCardEffect(object sender, EffectEventArgs e) { }



    public bool IsAttackTarget()
    {
        bool isTarget = true;
        Debug.Log("Checking if this card can be attacked");
        foreach (Transform child in board1)
        {
            Card card = child.GetComponent<Card>();
            if (card.KeyWords != null && card.KeyWords.Contains(KeyWord.Taunt) && this.KeyWords.Contains(KeyWord.Taunt) == false)
            {
                isTarget = false;
            }
           
        }
        return isTarget;
    }
            
        
    


    public virtual List<Card> GetEffectTargets()
    {
        List<Card> potentialTargets = new List<Card>();

        potentialTargets.AddRange(ListCardsInContainer(board1));
        potentialTargets.AddRange(ListCardsInContainer(board2));
        potentialTargets.AddRange(ListCardsInContainer(enemyBoard1));
        potentialTargets.AddRange(ListCardsInContainer(enemyBoard2));
        potentialTargets.Add(hero);
        potentialTargets.Add(enemyHero);


        return potentialTargets;
    }

    public virtual List<Card> GetAttackTargets()
    {
        List<Card> potentialTargets = new List<Card>();

        potentialTargets.AddRange(ListCardsInContainer(board1));
        potentialTargets.AddRange(ListCardsInContainer(board2));
        potentialTargets.AddRange(ListCardsInContainer(enemyBoard1));
        potentialTargets.AddRange(ListCardsInContainer(enemyBoard2));
        potentialTargets.Add(hero);
        potentialTargets.Add(enemyHero);

        return potentialTargets;
    }


    private void Start()
    {

        // Subscribes to runEffects
        runAfterEffects += AfterCardEffect;
        runWheneverEffects += WheneverCardEffect;

        SetInfo(new List<Card> { this });

        if (GetComponent<Stats>() != null)
        {
            stats = GetComponent<Stats>();

        }

    }

}








