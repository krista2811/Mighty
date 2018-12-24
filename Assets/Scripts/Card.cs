using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    public Dictionary<string, Sprite> Images { get; set; }
    public Sprite Front { get; set; }
    public Sprite Back { get; set; }
    public Suit CardSuit { get; set; }
    public Rank CardRank { get; set; }
    public Job CardJob { get; set; }
    public bool IsBack { get; set; }
    public int Point { get; set; }

    Sprite imageShow;

    // Use this for initialization
    void Start () {
        try {
            setImageShow(Images["back"]);
        } catch (KeyNotFoundException e) {
            Debug.Log(e);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    bool Flip(bool isBack) {
        IsBack = isBack;
        if (IsBack) {
            setImageShow(Images["back"]);
        } else {
            setImageShow(Images["front"]);
        }
        return IsBack;
    }

    Sprite getImage() {
        return imageShow;
    }

    void setImageShow(Sprite image) {
        imageShow = image;
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
