using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicCard {

    public class Card
    {
        public Dictionary<string, Sprite> Images { get; set; }
        public Suit CardSuit { get; set; }
        public Rank CardRank { get; set; }
        public Job CardJob { get; set; }
        public bool IsBack { get; set; }
        public float Point { get; set; }

        public bool IsClicked { get; set; }
        public bool IsHover { get; set; }

        Sprite imageShow;

        public Card()
        {
            IsClicked = false;
            IsHover = false;
        }

        public bool Flip(bool isBack)
        {
            IsBack = isBack;
            if (IsBack)
            {
                SetImageShow(Images["back"]);
            }
            else
            {
                int rank = (int)CardRank;
                SetImageShow(Images["front"]);
            }
            return IsBack;
        }

        public Sprite GetImage()
        {
            return imageShow;
        }

        public void SetInitShow()
        {
            Flip(IsBack);
        }

        void SetImageShow(Sprite image)
        {
            imageShow = image;
        }
    }


    public enum Suit
    {
        SPADE,
        DIAMOND,
        HEART,
        CLOVER,
        NONE
    }

    public enum Rank
    {
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

    public enum Job
    {
        NONE,
        JOKER,
        JOKER_CALL,
        MIGHTY
    }
}
