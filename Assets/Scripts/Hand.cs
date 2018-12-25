using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand {
    List<GameObject> CardObjects;
    bool IsBack;

    public Hand(bool isBack) {
        IsBack = isBack;
    }

    public void AddCard(GameObject OneCard) {
        OneCard.GetComponent<CardManager>().FlipCard(IsBack);
        CardObjects.Add(OneCard);
    }

    public int GetCardCount() {
        return CardObjects.Count;
    }
}
