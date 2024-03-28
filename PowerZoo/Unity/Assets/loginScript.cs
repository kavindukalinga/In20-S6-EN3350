using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class loginScript : MonoBehaviour
{

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

    public IEnumerator change_background(string url = Auth_API) {
        background_select(true, false, false, false);
        yield return StartCoroutine(player_authenticate(url));
        if (!string.IsNullOrEmpty(JWT_TOKEN)) {
            background_select(false, true, false, true);
        } else {
            background_select(false, false, true, false);
        }
    }

    public IEnumerator player_authenticate(string url = Auth_API) {

        // Create a JSON object representing your data
        string jsonData = "{\"apiKey\": \"" + API_KEY + "\"}";

        // Set the content type header to indicate JSON data
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST")) {
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError) {
                Debug.Log(request.error);
            } else {
                string jsonResponse = request.downloadHandler.text;
                TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(jsonResponse);
                JWT_TOKEN = tokenResponse.token;
            }
        }
    }

    private void background_select(bool load, bool success, bool error, bool text) {
        loadingBG.SetActive(load);
        loginSuccessBG.SetActive(success);
        loginErrorBG.SetActive(error);
        textTMP.SetActive(text);
    }
}