using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class APIHubScript : MonoBehaviour
{
    public PostMethod postMethod;
    public string API_KEY = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM2OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiYw";
    public string JWT_TOKEN;
    public string Response;
    private string Auth_API = "http://20.15.114.131:8080/api/login";
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";
    private string ViewPlayerList_API = "http://20.15.114.131:8080/api/user/profile/list";
    private string ViewYearlyPowerConsumption_API = "http://20.15.114.131:8080/api/power-consumption/yearly/view";
    private string ViewSpecificMonthConsumption_API = "http://20.15.114.131:8080/api/power-consumption/month/view";
    private string ViewCurrentMonthConsumption_API = "http://20.15.114.131:8080/api/power-consumption/current-month/view";
    private string ViewDailyConsumptionSpecificMonth_API = "http://20.15.114.131:8080/api/power-consumption/month/daily/view";
    private string ViewDailyConsumptionCurrentMonth_API = "http://20.15.114.131:8080/api/power-consumption/current-month/daily/view";

    public void Authenticate() => StartCoroutine(player_authenticate());
    public void ViewProfile() => StartCoroutine(get_request(ViewProfile_API));

    IEnumerator player_authenticate() {
        postMethod.outputArea.text = "Authenticating...";
        string url = Auth_API;

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
                postMethod.outputArea.text = request.error;
            } else {
                string jsonResponse = request.downloadHandler.text;
                // postMethod.outputArea.text = jsonResponse;
                TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(jsonResponse);
                JWT_TOKEN = tokenResponse.token;
            }
        }
    }


    public IEnumerator get_request(string url)
    {
        if (string.IsNullOrEmpty(JWT_TOKEN))
        {
            yield return StartCoroutine(player_authenticate());
        }
        Response = "";
        postMethod.outputArea.text = "Getting Player Details...";

        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + JWT_TOKEN);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                postMethod.outputArea.text = request.error;
            }
            else
            {
                string playerProfile = request.downloadHandler.text;
                postMethod.outputArea.text = playerProfile;
                Response = playerProfile;

                Debug.Log("Response: " + Response);
            }
        }
    }

}

[System.Serializable]
public class TokenResponse
{
    public string token;
}
