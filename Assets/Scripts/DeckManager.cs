using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BasicCard;

public class DeckManager : MonoBehaviour
{
    public static DeckManager GetDeckManager;

    public GameObject DeckObject;

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
        Hand handClass = new Hand();
        HandManager handManager = DeckObject.AddComponent<HandManager>();
        handManager.HandClass = handClass;
        handManager.HandObject = DeckObject;
        handManager.IsBack = true;
        handManager.Width = 0.01f;
        handManager.BlockAllTrigger();
        handManager.SetCardOrientation();

        cardFactory.GenerateAllCards();
        return new Deck
        {
            Cards = GetDeckHandManager().GetCardObjects()
        };
    }

    public void ResetDeck()
    {
        MyDeck = CreateDeck();
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

    public HandManager GetDeckHandManager() {
        return DeckObject.GetComponent<HandManager>();
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

    void AddOneSuit(Suit suit)
    {
        for (int i = 0; i < 13; i++)
        {
            int suitIndex = (int)suit;
            GameObject OneCard = CreateOneCard(
                SuitSprites[suitIndex][i], BackSprite, suit, SuitRanks[i], Job.NONE, SuitPoints[i]);
            DeckManager.GetDeckManager.GetDeckHandManager().AddCard(OneCard, false);
            //cardObjects.Add(OneCard);
        }
    }

    public void GenerateAllCards()
    {
        AddOneSuit(Suit.SPADE);
        AddOneSuit(Suit.DIAMOND);
        AddOneSuit(Suit.HEART);
        AddOneSuit(Suit.CLOVER);

        GameObject JokerObject = CreateOneCard(JokerSprite, BackSprite, Suit.NONE, Rank.ACE, Job.JOKER, -1);
        DeckManager.GetDeckManager.GetDeckHandManager().AddCard(JokerObject, false);

        // set Mighty and Joker Call
        ChangeJob(DeckManager.GetDeckManager.GetDeckHandManager().GetCardObjects(), 12, Job.MIGHTY);
        ChangeJob(DeckManager.GetDeckManager.GetDeckHandManager().GetCardObjects(), 40, Job.JOKER_CALL);

        return;
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
        
        OneCardManager.DefaultPosition = OneCard.transform.localPosition;
        OneCardManager.HoverPosition = OneCard.transform.localPosition;
        //OneCardManager.SetPositionHovered();

        // set Collider2D
        BoxCollider2D collider = OneCard.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(1.57f, 2.4f);
        collider.offset = new Vector2(0, 0.006f);

        // set Event Trigger in CardManager!!!

        return OneCard;
    }

}
