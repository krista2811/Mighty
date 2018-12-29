using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public Hand HandClass { get; set; }
    public GameObject HandObject { get; set; }

    public float Scale { get; set; }
    public float Width { get; set; }
    public bool IsBack { get; set; }
	// Use this for initialization
	void Start () {
        SpawnCards();
        //IsBack = false;
	}
	
	// Update is called once per frame
	void Update () {
        //SpawnCards();
    }

    void SpawnCards() {
        Vector3 parentPosition = HandObject.GetComponent<Transform>().position;
        Quaternion parentRotation = HandObject.GetComponent<Transform>().rotation;
        Vector3 parentScale = HandObject.GetComponent<Transform>().localScale;

        int count = HandClass.GetCardCount();
        if (count <= 0) {
            return;
        }
        float spawnDistance = Width / count;
        Debug.Log(spawnDistance);

        List<GameObject> cardObjects = HandClass.GetCardObjects();

        for (int i = 0; i < HandClass.GetCardCount(); i++) 
        {
            // Set component position!
            cardObjects[i].transform.parent = HandObject.transform;
            cardObjects[i].transform.rotation = new Quaternion();
            cardObjects[i].transform.localScale = new Vector3(1, 1, 1);

            Vector3 translate = new Vector3(
                -(Width / 2) + (spawnDistance * i),
                0,
                0.01f * i
            );

            cardObjects[i].transform.localPosition = translate;
            
            // Set Hover
            cardObjects[i].GetComponent<CardManager>().DefaultTransform = cardObjects[i].transform;
            cardObjects[i].GetComponent<CardManager>().SetPositionHovered();

            // flip card!
            Debug.Log(IsBack);
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

    public void AddCard(GameObject oneCard)
    {
        Debug.Log("ASDF");
        HandClass.AddCard(oneCard);
        SpawnCards();
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
}
