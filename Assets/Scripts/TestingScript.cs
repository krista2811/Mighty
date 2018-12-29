using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

    public GameObject OneCardImage;
    public GameObject HandObject;

    public bool flip;
    public bool white;
    
	// Use this for initialization
	void Start () {
        //Debug.Log(DeckManager.GetDeckManager.CreateDeck());
        //deck = DeckManager.GetDeckManager.CreateDeck();
        //OneCardImage = deck.Pop();

        Hand handClass = new Hand();
        HandManager handManager = HandObject.AddComponent<HandManager>();
        handManager.HandClass = handClass;
        handManager.HandObject = HandObject;
        handManager.IsBack = false;
        handManager.SetCardOrientation();
    }
	
	// Update is called once per frame
	void Update () {
        //try {
        //    OneCardImage.GetComponent<CardManager>().FlipCard(flip);
        //} catch(UnityException e) {

        //}
    }

    public void DrawCardToMyHand() {
        GameObject oneCardObject = DeckManager.GetDeckManager.Draw();
        HandObject.GetComponent<HandManager>().AddCard(oneCardObject, true);
    }

    public void GenerateDeck() {
        DeckManager.GetDeckManager.ResetDeck();
        OneCardImage.SetActive(false);
        //GameObject CardObject = DeckManager.GetDeckManager.Draw();
        //OneCardImage = CardObject.gameObject;
    }

    public void DrawCard() {
        OneCardImage.SetActive(false);
        OneCardImage = DeckManager.GetDeckManager.Draw();
    }

    public void ShuffleCard() {
        DeckManager.GetDeckManager.ShuffleDeck();
    }
}
