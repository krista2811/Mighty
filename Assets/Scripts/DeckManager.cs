using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    public Sprite[] SapSprites;
    public Sprite[] ClSprites;
    public Sprite[] HaSprites;
    public Sprite[] DaSprites;

    public Sprite BackSprite;
    public Sprite BackWhiteSprite;

    public Deck MyDeck;

    public bool IsBlack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Deck CreateDeck() {
        return null;
    }

    public void ResetDeck() {
        MyDeck = CreateDeck();
    }

    public void ShuffleDeck() {
        MyDeck.Shuffle();
    }

    public GameObject Draw() {
        return MyDeck.Pop();
    }


}

class CardFactory {
    static Sprite[][] SuitSprites { get; set; }
    static Sprite JokerSprite { get; set; }

    static Sprite BackSprite { get; set; }
    static Sprite BackWhiteSprite { get; set; }

    readonly static Rank[] SuitRanks = {
        Rank.TWO, Rank.THREE, Rank.FOUR, Rank.FIVE,
        Rank.SIX, Rank.SEVEN, Rank.EIGHT, Rank.NINE,
        Rank.TEN, Rank.JACK, Rank.QUEEN, Rank.KING,
        Rank.ACE
    };

    readonly float[] SuitPoints = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 1, 1, 1, 1
    };

    Card CardClassInit(Sprite Front, Sprite Back, Suit suit, Rank rank, Job job, float point)
    {
        Dictionary<string, Sprite> NewImage =
            new Dictionary<string, Sprite>();
        NewImage.Add("front", Front);
        NewImage.Add("back", Back);

        return new Card
        {
            Images = NewImage,
            CardSuit = suit,
            CardRank = rank,
            CardJob = job,
            Point = point,
            IsBack = true
        };
    }

    void AddOneSuit(Suit suit, List<GameObject> cardObjects) 
    {
        for (int i = 0; i < 13; i++) 
        {
            int suitIndex = (int)suit;
            cardObjects.Add(CreateOneCard(
                SuitSprites[suitIndex][i], BackSprite, suit, SuitRanks[i], Job.NONE, SuitPoints[i]));
        }
    }

    GameObject CreateOneCard(
        Sprite Front, Sprite Back, Suit suit, Rank rank, Job job, float point)
    {
        GameObject OneCard = new GameObject();

        Card OneCardClass = CardClassInit(Front, Back, suit, rank, job, point);
        CardManager OneCardManager = OneCard.AddComponent<CardManager>();
        OneCardManager.CardClass = OneCardClass;
        OneCardManager.CardImageObject = OneCard;

        return OneCard;
    }
}
