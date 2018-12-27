using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public Hand HandClass { get; set; }
    public GameObject HandObject { get; set; }

    public float Scale { get; set; }
    public float Width = 10;
    public bool IsBack { get; set; }
	// Use this for initialization
	void Start () {
        SpawnCards();
        IsBack = false;
	}
	
	// Update is called once per frame
	void Update () {
        //SpawnCards();
    }

    void SpawnCards() {
        Vector3 parentPosition = HandObject.GetComponent<Transform>().position;
        int count = HandClass.GetCardCount();
        if (count <= 0) {
            return;
        }
        float spawnDistance = Width / count;

        List<GameObject> cardObjects = HandClass.GetCardObjects();

        for (int i = 0; i < HandClass.GetCardCount(); i++) 
        {
            Vector3 childPosition = new Vector3();
            childPosition.x = parentPosition.x - 5 + spawnDistance * i;
            childPosition.y = parentPosition.y;
            childPosition.z = parentPosition.z + 0.01f * i;
            cardObjects[i].GetComponent<Transform>().position = childPosition;
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
        HandClass.AddCard(oneCard);
        SpawnCards();
    }
}
