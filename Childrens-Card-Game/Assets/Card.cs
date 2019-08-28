

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;
using static Variables;
using static GetSet;

public class Card : MonoBehaviour
{

    // For the 'before' cards.
    public Transform Allegiance;
    public Transform Container;
    public int Index;

    Transform hand { get { return Allegiance.GetChild(handIndex); } }
    Transform enemyHand { get { return GetContainer(this, false, handIndex); } }


    public Status status;
    public CardType type;
    public HashSet<Tribe> Tribes = new HashSet<Tribe> { };

    public Stats stats;

    public Card target;
    public Card targeter;
    public Card affects;
    public Card affecter;

    public virtual void AfterCardEffect(object sender, EffectEventArgs e) { }
    public virtual void WheneverCardEffect(object sender, EffectEventArgs e) { }

    public bool alreadySet = false;

    public virtual List<Card> GetEffectTargets()
    {
        List<Card> potentialTargets = new List<Card>();

        potentialTargets = ListCardsInContainer(Alliance.GetChild(board1Index));
        potentialTargets = ListCardsInContainer(Alliance.GetChild(board2Index));
        potentialTargets = ListCardsInContainer(Horde.GetChild(board1Index));
        potentialTargets = ListCardsInContainer(Horde.GetChild(board2Index));

        return potentialTargets;
    }

    public virtual List<Card> GetAttackTargets()
    {
        List<Card> potentialTargets = new List<Card>();

        potentialTargets.AddRange(ListCardsInContainer(Alliance.GetChild(board1Index)));
        potentialTargets.AddRange(ListCardsInContainer(Alliance.GetChild(board2Index)));
        potentialTargets.AddRange(ListCardsInContainer(Horde.GetChild(board1Index)));
        potentialTargets.AddRange(ListCardsInContainer(Horde.GetChild(board2Index)));
        potentialTargets.Add(Alliance.GetChild(heroIndex).GetComponent<Card>());

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








