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

    public Vector3 DefaultPosition { get; set; }
    public Vector3 HoverPosition { get; set; }

    public HandManager GetHandManager { get; set; }

    bool IsTrigger;

	// Use this for initialization
	void Start () {
        CardClass.SetInitShow();  // Initial card is "BACK"
        SetImageToCardObject();
	}

    public void SetPositionHovered() {
        Vector3 newHoverVector = new Vector3();
        newHoverVector.x = HoverPosition.x;
        newHoverVector.y = HoverPosition.y + 0.4f;
        newHoverVector.z = HoverPosition.z - 1.0f;
        HoverPosition = newHoverVector;
    }
	
	// Update is called once per frame
	void Update () {
        //SetImageToCardObject();
        if (CardClass.IsClicked)
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
        CardImageObject.GetComponent<SpriteRenderer>().sprite
                  = CardClass.GetImage();
    }

    public void ToggleCardIsClicked() {
        CardClass.IsClicked = !CardClass.IsClicked;
    }

    public void BlockTrigger() {
        IsTrigger = false;
    }

    public void AllowTrigger() {
        IsTrigger = true;
    }

    public void SetDown() {
        CardClass.IsSelected = false;
        CardClass.IsClicked = false;
        MakeDown();
    }

    void MakeUp()
    {
        CardImageObject.transform.localPosition = HoverPosition;
    }

    void MakeDown()
    {
        CardImageObject.transform.localPosition = DefaultPosition;
    }

    private void OnMouseDown()
    {
        if (!IsTrigger) {
            Debug.Log("Not Trigger Down!");
            return;
        }

        if (CardClass.IsClicked)
        {
            CardClass.IsSelected = true;

            // TODO: Set logic for selection!
            GetHandManager.PlayOneCard(gameObject);
        } else {
            GetHandManager.OneCardClicked();
            CardClass.IsClicked = true;
            GetHandManager.SelectedCard = gameObject;
        }
        //ToggleCardIsClicked();
    }

}
