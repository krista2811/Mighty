using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(StartCoroutine(GetGame()));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GetGame()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", formData);
        UnityWebRequest test = UnityWebRequest.Get("https://genne-mighty.run.goorm.io/game/phase");
        yield return test.SendWebRequest();

        if (test.isNetworkError || test.isHttpError)
        {
            Debug.Log(test.error);
        }
        else
        {
            Debug.Log("Network Succeed");
            Debug.Log("Phase: " + test.downloadHandler.text);
        }
    }
}
