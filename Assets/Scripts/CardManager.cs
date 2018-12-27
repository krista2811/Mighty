using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicCard;
/*
* This class is for Generating Card, Managing card. 
*/
public class CardManager : MonoBehaviour {
    public Card CardClass { get; set; }
    public GameObject CardImageObject { get; set; }
    public Vector3 PositionDefault { get; set; }
    public Vector3 PositionHovered { get; set; }

	// Use this for initialization
	void Start () {
        //CardClass.SetInitShow();  // Initial card is "BACK"
        SetImageToCardObject();
	}

    public void SetPositionHovered() {
        Vector3 newHoverVector = new Vector3();
        newHoverVector.x = PositionDefault.x;
        newHoverVector.y = PositionDefault.y + 0.4f;
        newHoverVector.z = PositionDefault.z;
        PositionHovered = newHoverVector;
    }
	
	// Update is called once per frame
	void Update () {
        //SetImageToCardObject();
        if (CardClass.IsHover || CardClass.IsClicked)
        {
            MakeUp();
        }
        else
        {
            MakeDown();
        }
    }

    public void FlipCard(bool isFlip) {
        CardClass.Flip(isFlip);
        SetImageToCardObject();
    }

    void SetImageToCardObject() {
        Debug.Log(CardClass.GetImage());
        Debug.Log(CardClass.CardRank);
        CardImageObject.GetComponent<SpriteRenderer>().sprite
                  = CardClass.GetImage();
    }

    public void ToggleCardIsClicked() {
        CardClass.IsClicked = !CardClass.IsClicked;
    }

    public void BlockTrigger() {
        CardImageObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public void AllowTrigger() {
        CardImageObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void MakeUp()
    {
        CardImageObject.GetComponent<Transform>().localPosition = PositionHovered;
    }

    void MakeDown()
    {
        CardImageObject.GetComponent<Transform>().localPosition = PositionDefault;
    }

    private void OnMouseEnter()
    {
        CardClass.IsHover = true;
    }

    private void OnMouseExit()
    {
        CardClass.IsHover = false;
    }

    private void OnMouseDown()
    {
        ToggleCardIsClicked();
    }

}
