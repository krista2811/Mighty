using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand {
    List<GameObject> CardObjects;

    public Hand() {
        CardObjects = new List<GameObject>();
    }

    public void AddCard(GameObject OneCard) {
        CardObjects.Add(OneCard);
    }

    public GameObject PopCard() {
        return RemoveCardAt(0);
    }

    public GameObject RemoveCardAt(int index) {
        GameObject oneCard = CardObjects[index];
        CardObjects.RemoveAt(index);
        return oneCard;
    }

    public int GetCardCount() {
        return CardObjects.Count;
    }

    public List<GameObject> GetCardObjects() {
        return CardObjects;
    }
}
