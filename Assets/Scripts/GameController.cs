using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static bool IsGameOver;
    public static GameController GetGameController;

    public GameObject MyHandObject;
    public GameObject[] YourObjects;

    public static int turn;

    HandFactory GetHandFactory;

    private void Awake()
    {
        IsGameOver = true;

        if (GetGameController == null) {
            GetGameController = this;
        } else if (GetGameController != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        GetHandFactory = new HandFactory();
        turn = 4;  // 4 is me

        HTTPManager hm = new HTTPManager();
        hm.Test_GetPhase();
    }

    // Update is called once per frame
    void Update () {
        if (IsGameOver) {
            return;
        }

        if (turn == 4) {
            SetMyTurn();
        } else {
            NotMyTurn();
        }
	}

    void SetMyTurn()
    {
        MyHandObject.GetComponent<HandManager>().AllowAllTrigger();
    }

    void NotMyTurn()
    {
        MyHandObject.GetComponent<HandManager>().BlockAllTrigger();
    }

    public void ResetGame() {
        GenerateDeck();
        DeckManager.GetDeckManager.ShuffleDeck();
        GetHandFactory.GenerateMyHand(MyHandObject);
        for (int i = 0; i < 4; i++)
        {
            GetHandFactory.GenerateYourHand(YourObjects[i]);
        }

        IsGameOver = false;
    }

    public void GenerateDeck()
    {
        DeckManager.GetDeckManager.ResetDeck();
    }

    public void DrawAll()
    {
        for (int i = 0; i < 50; i++) {
            DrawOne();
        }
    }

    public void DrawOne()
    {
        GameObject cardObject = DeckManager.GetDeckManager.Draw();
        if (turn == 4) {
            GetHandFactory.DrawCardToHand(MyHandObject, cardObject, true);
            MyHandObject.GetComponent<HandManager>().AllowAllTrigger();
        } else {
            GetHandFactory.DrawCardToHand(YourObjects[turn], cardObject, false);
            YourObjects[turn].GetComponent<HandManager>().BlockAllTrigger();
        }
        ChangeTurn();
    }

    public void ChangeTurn() {
        if (turn >= 4) {
            turn = 0;
        } else {
            turn += 1;
        }
    }

}


class HandFactory {
    public void GenerateMyHand(GameObject myHandObject)
    {
        Hand handClass = new Hand();
        HandManager handManager = myHandObject.AddComponent<HandManager>();
        handManager.HandClass = handClass;
        handManager.HandObject = myHandObject;
        handManager.IsBack = false;
        handManager.Width = 1.2f;
        handManager.AllowAllTrigger();
        handManager.SetCardOrientation();
    }

    public void GenerateYourHand(GameObject yourHandObject)
    {
        Hand handClass = new Hand();
        HandManager handManager = yourHandObject.AddComponent<HandManager>();
        handManager.HandClass = handClass;
        handManager.HandObject = yourHandObject;
        handManager.IsBack = true;
        handManager.Width = 0.5f;
        handManager.BlockAllTrigger();
        handManager.SetCardOrientation();
    }

    public void DrawCardToHand(GameObject handObject, GameObject cardObject, bool isTrigger) {
        handObject.GetComponent<HandManager>().AddCard(cardObject, isTrigger);
    }

}