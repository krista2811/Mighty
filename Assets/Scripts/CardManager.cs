using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This class is for Generating Card, Managing card. 
*/
public class CardManager : MonoBehaviour {
    public Card CardClass { get; set; }
    public GameObject CardImageObject { get; set; }

	// Use this for initialization
	void Start () {
        CardClass.SetInitShow();  // Initial card is "BACK"
        SetImageToCardObject();
	}
	
	// Update is called once per frame
	void Update () {
        SetImageToCardObject();
	}

    public void FlipCard(bool isFlip) {
        CardClass.Flip(isFlip);
    }

    void SetImageToCardObject() {
        CardImageObject.GetComponent<SpriteRenderer>().sprite
                  = CardClass.GetImage();
    }

    public void ToggleCardIsClicked() {
        CardClass.IsClicked = !CardClass.IsClicked;
    }

    private void OnMouseDown()
    {
        CardClass.Flip(true);
    }

    private void OnMouseUp()
    {
        CardClass.Flip(false);
    }

}
