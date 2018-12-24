using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    private Sprite front;
    private Sprite back;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum Suit {
    CLOVER,
    HEART,
    DIAMOND,
    SPADE,
    NONE
}

public enum Rank {
    TWO,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    TEN,
    JACK,
    QUEEN,
    KING,
    ACE
}

public enum Job {
    NONE,
    JOKER,
    JOKER_CALL,
    MIGHTY
}
