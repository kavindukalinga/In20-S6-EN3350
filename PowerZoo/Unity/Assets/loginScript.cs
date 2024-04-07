using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class loginScript : MonoBehaviour
{
    public APIHubScript APIHub;
    public string API_KEY = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM2OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiYw";
    public string JWT_TOKEN;
    public GameObject loadingBG;
    public GameObject loginSuccessBG;
    public GameObject loginErrorBG;
    public GameObject textTMP;
    private const string Auth_API = "http://20.15.114.131:8080/api/login";

    // Start is called before the first frame update
    void Start()
    {
        Authenticate();
    }

    public void Authenticate() => StartCoroutine(change_background());

    public IEnumerator change_background() {
        background_select(true, false, false, false);
        yield return StartCoroutine(APIHub.player_authenticate());
        if (!string.IsNullOrEmpty(APIHub.JWT_TOKEN)) {
            background_select(false, true, false, true);
        } else {
            background_select(false, false, true, false);
        }
    }

    private void background_select(bool load, bool success, bool error, bool text) {
        loadingBG.SetActive(load);
        loginSuccessBG.SetActive(success);
        loginErrorBG.SetActive(error);
        textTMP.SetActive(text);
    }
}