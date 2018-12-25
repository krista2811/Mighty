using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController GetGameController;

    private void Awake()
    {
        if (GetGameController == null) {
            GetGameController = this;
        } else if (GetGameController != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
