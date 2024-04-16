using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class APIHubScript : MonoBehaviour
{
    public string API_KEY = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM2OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiYw";
    public string JWT_TOKEN;
    public string Response;
    public QuizResponse quizResponse;
    public ScoreResponse scoreResponse;
    private string Auth_API = "http://20.15.114.131:8080/api/login";
    private string spring_Auth_API = "http://localhost:9000/auth/signup";
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";
    private string ViewPlayerList_API = "http://20.15.114.131:8080/api/user/profile/list";
    private string ViewYearlyPowerConsumption_API = "http://20.15.114.131:8080/api/power-consumption/yearly/view";
    private string ViewSpecificMonthConsumption_API = "http://20.15.114.131:8080/api/power-consumption/month/view";
    private string ViewCurrentMonthConsumption_API = "http://20.15.114.131:8080/api/power-consumption/current-month/view";
    private string ViewDailyConsumptionSpecificMonth_API = "http://20.15.114.131:8080/api/power-consumption/month/daily/view";
    private string ViewDailyConsumptionCurrentMonth_API = "http://20.15.114.131:8080/api/power-consumption/current-month/daily/view";
    private string isQuizCompleted_API = "http://localhost:9000/accessed/isAnswered";
    private string redirectQuiz_API = "http://localhost:5173";
    private string getScore_API = "http://localhost:9000/get-score";

    public void Authenticate() => StartCoroutine(player_authenticate());
    public void ViewProfile() => StartCoroutine(get_request(ViewProfile_API));
    public void ViewPlayerList() => StartCoroutine(get_request(ViewPlayerList_API));
    public void ViewYearlyPowerConsumption() => StartCoroutine(get_request(ViewYearlyPowerConsumption_API));
    public void ViewSpecificMonthConsumption() => StartCoroutine(get_request(ViewSpecificMonthConsumption_API));
    public void ViewCurrentMonthConsumption() => StartCoroutine(get_request(ViewCurrentMonthConsumption_API));
    public void ViewDailyConsumptionSpecificMonth() => StartCoroutine(get_request(ViewDailyConsumptionSpecificMonth_API));
    public void ViewDailyConsumptionCurrentMonth() => StartCoroutine(get_request(ViewDailyConsumptionCurrentMonth_API));
    public void CheckQuizCompleted() => StartCoroutine(check_quiz_completed());
    public void RedirectQuiz() => StartCoroutine(redirectQuiz());

    public IEnumerator check_quiz_completed()
    {
        yield return StartCoroutine(get_request(isQuizCompleted_API));
        if (string.IsNullOrEmpty(Response))
        {
            Debug.LogError("Response is null.");
            quizResponse.isAnswered = false;
        }
        else {
            quizResponse = JsonUtility.FromJson<QuizResponse>(Response);
        }
    }

    public IEnumerator player_authenticate() { // POST request
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

            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError) {
                Debug.LogError("Can not Authenticate.");
            } else {
                string jsonResponse = request.downloadHandler.text;
                TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(jsonResponse);
                JWT_TOKEN = tokenResponse.token;
            }
        }
    }

    public IEnumerator player_authenticate_spring() { // POST request
        string url = spring_Auth_API;
        
        yield return StartCoroutine(get_request(ViewProfile_API));
        if (Response != null) {
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(Response);
            string username = playerData.user.username;
        
            // Create a JSON object representing your data
            string jsonData = "{\"login\": \"" + username + "\", \"password\": " + "1234" + "}";

            // Set the content type header to indicate JSON data
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
            using (UnityWebRequest request = new UnityWebRequest(url, "POST")) {
                request.uploadHandler = new UploadHandlerRaw(postData);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError) {
                    Debug.LogError("Can not Authenticate with springBoot Backend");
                } else {
                    string jsonResponse = request.downloadHandler.text;
                    TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(jsonResponse);
                    Debug.Log("Spring response: " + tokenResponse.token);
                }
            }
        } else {
            Debug.LogError("Token response is null.");
        }
    }

    public IEnumerator get_request(string url)
    {
        if (string.IsNullOrEmpty(JWT_TOKEN))
        {
            yield return StartCoroutine(player_authenticate());
        }
        Response = "";

        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + JWT_TOKEN);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Response = request.downloadHandler.text;
                Debug.Log("Response: " + Response);
            }
        }
    }

    public IEnumerator put_request(string url, PlayerData playerData) {
        if (string.IsNullOrEmpty(JWT_TOKEN))
        {
            yield return StartCoroutine(player_authenticate());
        }
        Response = "";
        using (UnityWebRequest request = new UnityWebRequest(url, "PUT")) {
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(playerData.user)));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + JWT_TOKEN);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Response = jsonResponse;
                Debug.Log("Response: " + Response);
            }
        }
    }

    public IEnumerator get_score() {
        yield return StartCoroutine(get_request(getScore_API));
        if (string.IsNullOrEmpty(Response))
        {
            scoreResponse.score = 0;
            Debug.LogError("Response is null.");
        }
        else {
            scoreResponse = JsonUtility.FromJson<ScoreResponse>(Response);
            Debug.Log("Score: " + scoreResponse.score);
        }
        
    }

    public IEnumerator redirectQuiz() {
        Application.OpenURL(redirectQuiz_API);
        yield return null;
    }

}

[System.Serializable]
public class TokenResponse
{
    public string token;
}

[System.Serializable]
public class QuizResponse
{
    public bool isAnswered;
}

[System.Serializable]
public class ScoreResponse
{
    public int score;
}

[System.Serializable]
public class PlayerData
{
    public UserDataFromServer user;
}

[System.Serializable]
public class UserDataFromServer
{
    public string firstname;
    public string lastname;
    public string username;
    public string nic;
    public string phoneNumber;
    public string email;
    // public string profilePictureUrl;
}
