using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager GetDeckManager;

    public Sprite[] SapSprites;
    public Sprite[] ClSprites;
    public Sprite[] HaSprites;
    public Sprite[] DaSprites;

    public Sprite JokerSprite;

    public Sprite BackSprite;
    public Sprite BackWhiteSprite;

    public Deck MyDeck;

    public bool IsBlack;

    CardFactory cardFactory;

    // Use this for initialization
    void Start()
    {
    }

    void Awake()
    {
        //If we don't currently have a game control...
        if (GetDeckManager == null) {
            Sprite[][] CardSprites = {
                SapSprites,
                DaSprites,
                HaSprites,
                ClSprites
            };

            cardFactory = new CardFactory
            {
                SuitSprites = CardSprites,
                JokerSprite = JokerSprite,
                BackSprite = BackSprite
            };
            GetDeckManager = this;
        }
        //...set this one to be it...
        //...otherwise...
        else if (GetDeckManager != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBlack) {
            cardFactory.BackSprite = BackWhiteSprite;
        } else {
            cardFactory.BackSprite = BackSprite;
        }
    }

    Deck CreateDeck()
    {
        List<GameObject> CardObjects;
        CardObjects = cardFactory.GenerateAllCards();
        Debug.Log(CardObjects.Count);
        return new Deck
        {
            Cards = CardObjects
        };
    }

    public void ResetDeck()
    {
        MyDeck = CreateDeck();
        Debug.Log(MyDeck.GetDeckSize());
    }

    public void ShuffleDeck()
    {
        MyDeck.Shuffle();
    }

    public GameObject Draw()
    {
        if (MyDeck.GetDeckSize() <= 0) {
            Debug.LogError("Index over");
            return null;
        }
        GameObject DrawedCard = MyDeck.Pop();
        DrawedCard.SetActive(true);
        return DrawedCard;
    }


}

class CardFactory
{
    public Sprite[][] SuitSprites { get; set; }
    public Sprite JokerSprite { get; set; }

    public Sprite BackSprite { get; set; }

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
            GameObject OneCard = CreateOneCard(
                SuitSprites[suitIndex][i], BackSprite, suit, SuitRanks[i], Job.NONE, SuitPoints[i]);
            OneCard.SetActive(false);
            cardObjects.Add(OneCard);
        }
    }

    public List<GameObject> GenerateAllCards()
    {
        List<GameObject> NewCardDeck = new List<GameObject>();
        AddOneSuit(Suit.SPADE, NewCardDeck);
        AddOneSuit(Suit.DIAMOND, NewCardDeck);
        AddOneSuit(Suit.HEART, NewCardDeck);
        AddOneSuit(Suit.CLOVER, NewCardDeck);

        GameObject JokerObject = CreateOneCard(JokerSprite, BackSprite, Suit.NONE, Rank.ACE, Job.JOKER, -1);
        JokerObject.SetActive(false);
        NewCardDeck.Add(JokerObject);

        // set Mighty and Joker Call
        ChangeJob(NewCardDeck, 12, Job.MIGHTY);
        ChangeJob(NewCardDeck, 40, Job.JOKER_CALL);

        return NewCardDeck;
    }

    void ChangeJob(List<GameObject> cardObjects, int cardIndex, Job job)
    {
        cardObjects[cardIndex].GetComponent<CardManager>().CardClass.CardJob = job;
    }

    GameObject CreateOneCard(
        Sprite Front, Sprite Back, Suit suit, Rank rank, Job job, float point)
    {
        GameObject OneCard = new GameObject();

        Card OneCardClass = CardClassInit(Front, Back, suit, rank, job, point);
        CardManager OneCardManager = OneCard.AddComponent<CardManager>();
        OneCard.AddComponent<SpriteRenderer>();
        OneCardManager.CardClass = OneCardClass;
        OneCardManager.CardImageObject = OneCard;

        return OneCard;
    }
}
