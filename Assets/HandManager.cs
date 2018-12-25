using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public Hand HandClass { get; set; }
    public GameObject HandObject { get; set; }

    public float Scale { get; set; }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnCards() {

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
}
