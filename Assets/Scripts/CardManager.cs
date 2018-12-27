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

    public Transform DefaultTransform { get; set; }
    public Transform HoverTransform { get; set; }

	// Use this for initialization
	void Start () {
        CardClass.SetInitShow();  // Initial card is "BACK"
        SetImageToCardObject();
	}

    public void SetPositionHovered() {
        if (HoverTransform == null) {
            HoverTransform = CardImageObject.transform;
        }

        Vector3 newHoverVector = new Vector3();
        newHoverVector.x = DefaultTransform.position.x;
        newHoverVector.y = DefaultTransform.position.y + 0.4f;
        newHoverVector.z = DefaultTransform.position.z;
        HoverTransform.position = newHoverVector;
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
        CardImageObject.transform.position = HoverTransform.position;
    }

    void MakeDown()
    {
        CardImageObject.transform.position = DefaultTransform.position;
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
