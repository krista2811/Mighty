using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPManager : MonoBehaviour {
    public static HTTPManager GetHTTPManager;

    public delegate void Callback<T_param>(T_param param);


    private void Awake()
    {
        if (GetHTTPManager == null) {
            GetHTTPManager = this;
        } else if (GetHTTPManager != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Get(string uri, Callback<UnityWebRequest> callback) {
        UnityWebRequest req = UnityWebRequest.Get(uri);
        yield return req.SendWebRequest();

        ResolveCallback(req, callback);
    }

    public void GetData() {
        Callback<UnityWebRequest> callback = new Callback<UnityWebRequest>(Test_Callback);
        StartCoroutine(Get(Api.GetGameData(), callback));
    }

    public void Test_GetPhase()
    {
        Callback<UnityWebRequest> test_callback = new Callback<UnityWebRequest>(Test_Callback);
        StartCoroutine(Get(Api.GetGamePhase(), test_callback));
    }

    void Test_Callback(UnityWebRequest sth)
    {
        Debug.Log(sth.downloadHandler.text);
    }

    void ResolveCallback(UnityWebRequest req, Callback<UnityWebRequest> callback)
    {
        // check error of request. If error, send Error.
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            callback(req);
        }
    }
}

public static class Api {
    static string G_HOST = "https://genne-mighty.run.goorm.io";
    static string A_HOST = "현재 없음";

    static string GAME = "game";

    static string PHASE = "game/phase";

    static string GenerateUri(string host, string func, string sub) {
        return host + "/" + func + "/" + sub;
    }

    public static string GetGamePhase() {
        return GenerateUri(G_HOST, GAME, "phase");
    }

    public static string GetGameData() {
        return GenerateUri(G_HOST, GAME, "data");
    }
}
