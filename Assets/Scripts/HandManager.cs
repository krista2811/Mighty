using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public Hand HandClass { get; set; }
    public GameObject HandObject { get; set; }

    public float Scale { get; set; }
    public float Width { get; set; }
    public bool IsBack { get; set; }

    public static GameObject SelectedCard { get; set; }

	// Use this for initialization
	void Start () {
        SpawnCards();
        //IsBack = false;
	}
	
	// Update is called once per frame
	void Update () {
        //SpawnCards();
    }

    public void SetAllDown() {
        List<GameObject> cardObjects = HandClass.GetCardObjects();

        foreach (GameObject cardObject in cardObjects)
        {
            cardObject.GetComponent<CardManager>().SetDown();
        }
    }

    void SpawnCards() {
        Vector3 parentPosition = HandObject.GetComponent<Transform>().position;
        Quaternion parentRotation = HandObject.GetComponent<Transform>().rotation;
        Vector3 parentScale = HandObject.GetComponent<Transform>().localScale;

        int count = HandClass.GetCardCount();
        if (count <= 0) {
            return;
        }
        float spawnDistance = Width * count;

        List<GameObject> cardObjects = HandClass.GetCardObjects();

        for (int i = 0; i < HandClass.GetCardCount(); i++) 
        {
            // Set component position!
            cardObjects[i].transform.parent = HandObject.transform;
            cardObjects[i].transform.rotation = new Quaternion();
            cardObjects[i].transform.localScale = new Vector3(1, 1, 1);

            Vector3 translate = new Vector3(
                - (spawnDistance / 2) + (Width * i),
                0,
                1 - 0.01f * i
            );

            cardObjects[i].transform.localPosition = translate;
            
            // Set Hover
            cardObjects[i].GetComponent<CardManager>().DefaultPosition = cardObjects[i].transform.localPosition;
            cardObjects[i].GetComponent<CardManager>().HoverPosition = cardObjects[i].transform.localPosition;
            cardObjects[i].GetComponent<CardManager>().SetPositionHovered();


            // flip card!
            cardObjects[i].GetComponent<CardManager>().FlipCard(IsBack);
        }
    }

    void MakeSelected(GameObject oneCard) {
        SetCardYPosition(oneCard, 10);
    }

    void MakeUnselected(GameObject oneCard) {
        SetCardYPosition(oneCard, -10);
    }

    void SetCardYPosition(GameObject oneCard, float amount) {
        Vector3 position = oneCard.GetComponent<Transform>().localPosition;
        position.y += amount;
        oneCard.GetComponent<Transform>().localPosition = position;
    }

    public void AddCard(GameObject oneCard, bool isTrigger)
    {
        oneCard.GetComponent<CardManager>().GetHandManager = gameObject.GetComponent<HandManager>();
        HandClass.AddCard(oneCard);
        SpawnCards();
        if (isTrigger) {
            AllowAllTrigger();
        } else {
            BlockAllTrigger();
        }
    }

    public void BlockAllTrigger() {
        HandClass.BlockAll();
    }

    public void AllowAllTrigger() {
        HandClass.AllowAll();
    }

    public void SetCardOrientation()
    {
        HandClass.FlipAll(IsBack);
    }

    public void OneCardClicked() {
        // callback function in One Card Click!
        SetAllDown();
    }
}
