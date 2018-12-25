using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

    public GameObject OneCardImage;
    public Sprite Front;
    public Sprite Back;
    public Sprite WhiteBack;

    public bool flip;
    public bool white;

    Card cardClass;
    Dictionary<string, Sprite> newImage = 
        new Dictionary<string, Sprite>();

	// Use this for initialization
	void Start () {
        newImage.Add("front", Front);
        newImage.Add("back", Back);

        cardClass = new Card
        {
            Images = newImage,
            CardSuit = Suit.CLOVER,
            CardRank = Rank.ACE,
            CardJob = Job.NONE,
            Point = 1,
            IsBack = true
        };
		
	}
	
	// Update is called once per frame
	void Update () {
        cardClass.Flip(flip);
        if (white) {
            newImage["back"] = WhiteBack;
        } else {
            newImage["back"] = Back;
        }
        cardClass.Images = newImage;

        OneCardImage.GetComponent<SpriteRenderer>().sprite 
                    = cardClass.getImage();
    }
}
