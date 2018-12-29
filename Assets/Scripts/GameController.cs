using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController GetGameController;
    public GameObject MyHandObject;
    public GameObject[] YourObjects;

    public static int turn;

    HandFactory GetHandFactory;

    private void Awake()
    {
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
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ResetGame() {
        GenerateDeck();
        DeckManager.GetDeckManager.ShuffleDeck();
        GetHandFactory.GenerateMyHand(MyHandObject);
        for (int i = 0; i < 4; i++)
        {
            GetHandFactory.GenerateYourHand(YourObjects[i]);
        }
    }

    public void GenerateDeck()
    {
        DeckManager.GetDeckManager.ResetDeck();
    }

    public void DrawAll()
    {

    }

    public void DrawOne()
    {
        GameObject cardObject = DeckManager.GetDeckManager.Draw();
        if (turn == 4) {
            GetHandFactory.DrawCardToHand(MyHandObject, cardObject);
        } else {
            GetHandFactory.DrawCardToHand(YourObjects[turn], cardObject);
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
        handManager.Width = 10;
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
        handManager.Width = 5;
        handManager.BlockAllTrigger();
        handManager.SetCardOrientation();
    }

    public void DrawCardToHand(GameObject handObject, GameObject cardObject) {
        handObject.GetComponent<HandManager>().AddCard(cardObject);
    }
}