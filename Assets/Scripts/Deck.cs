using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    public Dictionary<string, Sprite> Images { get; set; }
    public List<Card> Cards { get; set; }

    private bool isShuffle;

	// Use this for initialization
	void Start () {
        isShuffle = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int GetDeckSize() {
        return Cards.Count;
    }

    public void Shuffle() {
        ShuffleList<Card>(Cards);
    }

    private void Reset()
    {
        // TODO: make 53 cards.
    }

    private static void ShuffleList<T>(List<T> list)
    {
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
