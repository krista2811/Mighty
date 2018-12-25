using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {
    public Dictionary<string, Sprite> Images { get; set; }
    public Suit CardSuit { get; set; }
    public Rank CardRank { get; set; }
    public Job CardJob { get; set; }
    public bool IsBack { get; set; }
    public float Point { get; set; }

    Sprite imageShow;

    public bool Flip(bool isBack) {
        IsBack = isBack;
        if (IsBack) {
            SetImageShow(Images["back"]);
        } else {
            SetImageShow(Images["front"]);
        }
        return IsBack;
    }

    public Sprite GetImage() {
        return imageShow;
    }

    public void SetInitShow() {
        Flip(true);
    }

    void SetImageShow(Sprite image) {
        imageShow = image;
    }
}

public enum Suit {
    SPADE,
    DIAMOND,
    HEART,
    CLOVER,
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
