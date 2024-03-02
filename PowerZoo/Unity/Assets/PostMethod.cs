using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostMethod : MonoBehaviour
{
    InputField outputArea;
    public string API_KEY = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM2OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiYw";

    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(PostData);
        PostData();
    }

    void PostData() => StartCoroutine(player_authenticate());

    IEnumerator player_authenticate() {
        outputArea.text = "Sending data to server...";
        string url = "http://20.15.114.131:8080/api/login";

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
                outputArea.text = request.error;
            } else {
                outputArea.text = request.downloadHandler.text;
            }
        }
    }

}
