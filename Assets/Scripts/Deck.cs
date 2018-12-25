using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {
    public Dictionary<string, Sprite> Images { get; set; }
    public List<GameObject> Cards { get; set; }

    private bool IsShuffle { get; set; }

    private int GetDeckSize() {
        return Cards.Count;
    }

    public void Shuffle() {
        ShuffleList<GameObject>(Cards);
    }

    public GameObject Pop() {
        // Used in GameManager Draw
        GameObject Card = Cards[0];
        Cards.RemoveAt(0);
        return Card;
    }

    private static void ShuffleList<T>(List<T> list)
    {
        // helper function
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }
}
