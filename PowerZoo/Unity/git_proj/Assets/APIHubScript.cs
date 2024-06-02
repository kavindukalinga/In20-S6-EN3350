using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Diagnostics;

public class APIHubScript : MonoBehaviour
{
    public static APIHubScript Instance { get; private set; }
    public string API_KEY = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM2OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiYw";
    public string JWT_TOKEN;
    public string Response;
    public string username;
    public QuizResponse quizResponse;
    public ScoreResponse scoreResponse;
    public CoinResponse coinResponse;
    public DailyPower dailyPower;
    public PlayerData playerData;
    public CurrentUnits currentUnits;
    public LastLogging lastLogging;
    public AnimalsCount animalsCount;
    public int stallLevel;
    private string Auth_API = "http://20.15.114.131:8080/api/login";
    private string spring_Auth_API = "http://localhost:9000/auth/signup";
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";
    private string ViewPlayerList_API = "http://20.15.114.131:8080/api/user/profile/list";
    private string ViewYearlyPowerConsumption_API = "http://20.15.114.131:8080/api/power-consumption/yearly/view";
    private string ViewSpecificMonthConsumption_API = "http://20.15.114.131:8080/api/power-consumption/month/view";
    private string ViewCurrentMonthConsumption_API = "http://20.15.114.131:8080/api/power-consumption/current-month/view";
    private string ViewDailyConsumptionSpecificMonth_API = "http://20.15.114.131:8080/api/power-consumption/month/daily/view";
    private string ViewDailyConsumptionCurrentMonth_API = "http://20.15.114.131:8080/api/power-consumption/current-month/daily/view";
    private string ViewCurrentUnits_API = "http://20.15.114.131:8080/api/power-consumption/current/view";
    private string isQuizCompleted_API = "http://localhost:9000/accessed/isAnswered/";
    private string redirectQuiz_API = "http://localhost:5173/user/";
    private string getScore_API = "http://localhost:9000/accessed/finalscore/";
    private string getCoins_API = "http://localhost:9000/api/players/coins";
    private string getAnimals_API = "http://localhost:9000/api/animals/counts";
    private string lastLogging_API = "http://localhost:9000/api/players/time";
    private string putCoins_API = "http://localhost:9000/api/players/coins?coins=";
    private string getStall_API = "http://localhost:9000/api/stalls/stall";
    private string addFood_API = "http://localhost:9000/api/foods/";
    private string addAnimal_API = "http://localhost:9000/api/animals/add/";
    // private string updateStall_API = "http://localhost:9000/api/stalls/stall";
    // private string getScore_API = "http://127.0.0.1:5000/get-score";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (string.IsNullOrEmpty(username))
        {
            yield return StartCoroutine(get_username());
        }
        string url = isQuizCompleted_API + username;
        UnityEngine.Debug.Log("Check quiz URL: " + url);
        yield return StartCoroutine(get_request(url));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Response is null.");
            quizResponse.isAnswered = false;
        }
        else
        {
            quizResponse = JsonUtility.FromJson<QuizResponse>(Response);
        }
    }

    public IEnumerator player_authenticate()
    { // POST request
        string url = Auth_API;

        // Create a JSON object representing your data
        string jsonData = "{\"apiKey\": \"" + API_KEY + "\"}";

        // Set the content type header to indicate JSON data
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
            {
                //UnityEngine.Debug.LogError("Can not Authenticate.");
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(jsonResponse);
                JWT_TOKEN = tokenResponse.token;
            }
        }
    }

    public IEnumerator player_authenticate_spring()
    { // POST request
        string url = spring_Auth_API;
        UnityEngine.Debug.Log("Spring BackEnd URL: " + url);

        yield return StartCoroutine(get_request(ViewProfile_API));
        if (Response != null)
        {
            playerData = JsonUtility.FromJson<PlayerData>(Response);
            username = playerData.user.username;

            // Create a JSON object representing your data
            string jsonData = "{\"login\": \"" + username + "\", \"password\": \"" + API_KEY + "\"}";
            UnityEngine.Debug.Log("JSON Body for Spring: " + jsonData);

            // Set the content type header to indicate JSON data
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(postData);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
                {
                    UnityEngine.Debug.LogError("Can not Authenticate with springBoot Backend");
                }
                else
                {
                    string jsonResponse = request.downloadHandler.text;
                    TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(jsonResponse);
                    UnityEngine.Debug.Log("Spring response: " + tokenResponse.token);
                }
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Token response is null.");
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
            if (!url.Contains("localhost")) { request.SetRequestHeader("Authorization", "Bearer " + JWT_TOKEN); }
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError(request.error);
            }
            else
            {
                Response = request.downloadHandler.text;
                UnityEngine.Debug.Log("Response: " + Response);
            }
        }
    }

    public IEnumerator put_request(string url, PlayerData playerData)
    {
        if (string.IsNullOrEmpty(JWT_TOKEN))
        {
            yield return StartCoroutine(player_authenticate());
        }
        Response = "";
        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
        {
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(playerData.user)));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + JWT_TOKEN);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Response = jsonResponse;
                UnityEngine.Debug.Log("Response: " + Response);
            }
        }
    }

    public IEnumerator put_request_string(string url)
    {
        Response = "";
        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
        {
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(playerData.user)));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Response = jsonResponse;
            }
        }
    }

    public IEnumerator get_score()
    {
        if (string.IsNullOrEmpty(username))
        {
            yield return StartCoroutine(get_username());
        }
        string url = getScore_API + username;
        UnityEngine.Debug.Log("Score URL: " + url);
        yield return StartCoroutine(get_request(url));
        if (string.IsNullOrEmpty(Response))
        {
            scoreResponse.score = 0;
            UnityEngine.Debug.LogError("Response is null.");
        }
        else
        {
            scoreResponse = JsonUtility.FromJson<ScoreResponse>(Response);
            UnityEngine.Debug.Log("Score: " + scoreResponse.score);
        }
    }

    public IEnumerator get_coins()
    {
        string url = getCoins_API;
        UnityEngine.Debug.Log("Coins URL: " + url);
        yield return StartCoroutine(get_request(url));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Response is null.");
        }
        else
        {
            coinResponse = JsonUtility.FromJson<CoinResponse>(Response);
            UnityEngine.Debug.Log("Coins: " + coinResponse.coins);
        }
    }

    public IEnumerator get_animals()
    {
        yield return StartCoroutine(get_request(getAnimals_API));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Response is null.");
        }
        else
        {
            animalsCount = JsonConvert.DeserializeObject<AnimalsCount>(Response);
        }
    }

    public IEnumerator redirectQuiz()
    {
        if (string.IsNullOrEmpty(username))
        {
            yield return StartCoroutine(get_username());
        }
        string url = redirectQuiz_API + username + "/" + API_KEY;
        Application.OpenURL(url);
        yield return null;
    }

    private IEnumerator get_username()
    {
        yield return StartCoroutine(get_request(ViewProfile_API));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Response is null.");
        }
        else
        {
            playerData = JsonUtility.FromJson<PlayerData>(Response);
            username = playerData.user.username;
        }
    }

    public IEnumerator get_daily_power()
    {
        yield return StartCoroutine(get_request(ViewDailyConsumptionCurrentMonth_API));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Response is null.");
        }
        else
        {
            dailyPower = JsonConvert.DeserializeObject<DailyPower>(Response);
            // UnityEngine.Debug.Log("Daily Power: " + dailyPower.dailyPowerConsumptionView.dailyUnits);
        }
    }

    public IEnumerator get_current_units()
    {
        yield return StartCoroutine(get_request(ViewCurrentUnits_API));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Response is null.");
        }
        else
        {
            currentUnits = JsonConvert.DeserializeObject<CurrentUnits>(Response);
            // UnityEngine.Debug.Log("Current Units: " + currentUnits.currentConsumption);
        }
    }

    public IEnumerator put_last_logging()
    {
        yield return StartCoroutine(put_request_string(lastLogging_API));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Response is null.");
        }
        else
        {
            UnityEngine.Debug.Log("Last Logging original: " + Response);
            lastLogging = JsonConvert.DeserializeObject<LastLogging>(Response);
            UnityEngine.Debug.Log("Last Logging: " + lastLogging.oldTime);
        }
    }

    public IEnumerator put_coins(float coins)
    {
        string url = putCoins_API + ((int)coins).ToString();
        yield return StartCoroutine(put_request_string(url));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Coins did not updated.");
        }
        else
        {
            UnityEngine.Debug.Log("Coins updated");
        }
    }

    public IEnumerator get_stall_level(int stall_num)
    {
        string url = getStall_API + stall_num.ToString();
        yield return StartCoroutine(get_request(url));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Coins did not updated.");
        }
        else
        {
            UnityEngine.Debug.Log("Stall Level: " + Response);
            int parsedStallLevel;
            if (int.TryParse(Response, out parsedStallLevel))
            {
                stallLevel = parsedStallLevel;
            }
            else
            {
                UnityEngine.Debug.LogError("Failed to parse Response as integer.");
            }
        }
    }

    public IEnumerator put_stall_level(int stall_num, int stall_level)
    {
        string url = getStall_API + stall_num.ToString() + "?level=" + stall_level.ToString();
        yield return StartCoroutine(put_request_string(url));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Level did not updated.");
        }
        else
        {
            UnityEngine.Debug.Log("Level Updated.");
        }
    }

    public IEnumerator add_food(int foodID)
    {
        string[] foods = new string[] {
            "hay",
            "grass",
            "meat",
            "fish",
            "banana",
            "milk",
            "bamboo",
            "honey",
            "magic spell"
        };
        string food = foods[foodID];
        string url = addFood_API + food + "?value=1";
        yield return StartCoroutine(put_request_string(url));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Food did not added.");
        }
        else
        {
            UnityEngine.Debug.Log("Food Added.");
        }
    }

    public IEnumerator add_animal(int animalID)
    {
        string[] animals = new string[] {
            "elephant",
            "lion",
            "tiger",
            "giraffe",
            "zebra"
        };
        string[] animals_health = new string[] {
            "20000",
            "12000",
            "11000",
            "14000",
            "5000"
        };
        string animal = animals[animalID];
        string health = animals_health[animalID];
        string url = addAnimal_API + animal + "?newHealthValue=" + health;
        yield return StartCoroutine(put_request_string(url));
        if (string.IsNullOrEmpty(Response))
        {
            UnityEngine.Debug.LogError("Animal did not added.");
        }
        else
        {
            UnityEngine.Debug.Log("Animal Added.");
        }
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
public class CoinResponse
{
    public int coins;
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

[System.Serializable]
public class DailyPowerConsumptionView
{
    public int year;
    public int month;
    public Dictionary<string, float> dailyUnits;
}

[System.Serializable]
public class DailyPower
{
    public DailyPowerConsumptionView dailyPowerConsumptionView;
}

[System.Serializable]
public class CurrentUnits
{
    public float currentConsumption;
}

[System.Serializable]
public class AnimalsCount
{
    public int elephant;
    public int lion;
    public int tiger;
    public int giraffe;
    public int zebra;
}

[System.Serializable]
public class LastLogging
{
    public DateTime oldTime;
    public DateTime newTime;
}

[System.Serializable]
public class StallLevel
{
    public int level;
}