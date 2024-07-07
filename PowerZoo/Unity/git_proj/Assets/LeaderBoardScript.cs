using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;

public class LeaderBoardScript : MonoBehaviour
{
    // TextMeshPro fields for displaying ranks, scores, usernames, and comments
    public TMP_Text Rank1, Rank2, Rank3, Rank4, Rank5, Rank6, Rank7, Rank8, Rank9, Rank10;
    public TMP_Text Score1, Score2, Score3, Score4, Score5, Score6, Score7, Score8, Score9, Score10;
    public TMP_Text userName1, userName2, userName3, userName4, userName5, userName6, userName7, userName8, userName9, userName10;
    public TMP_Text comment;
    public TMP_Text Coins;

    // Private variables for storing data
    private float currentCoins;
    private string data;
    private string units;
    private double Current_units;
    private string Myusername;
    private PlayerData playerData;
    private int MyRank;

    // Dictionaries for storing player list and consumption data
    private Dictionary<string, object> playerlist;
    private Dictionary<string, double> consumption;
    private Dictionary<string, double> consumption_dic = new Dictionary<string, double>();
    private Dictionary<string, double> score_dic = new Dictionary<string, double>();
    private List<List<TMP_Text>> text_labels;

    // API endpoints for retrieving data
    private string ViewPlayerList_API = "http://20.15.114.131:8080/api/user/profile/list";
    private string ViewCurrentUnits_API = "http://20.15.114.131:8080/api/power-consumption/current/view";
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";

    void Start()
    {
        //initial ranking
        initialRanking();
        // Initialize leaderboard data retrieval
        StartCoroutine(InitializeLeaderboard());
    }

    // Coroutine to initialize leaderboard data
    private IEnumerator InitializeLeaderboard()
    {
        yield return StartCoroutine(get_player_profile());
        yield return StartCoroutine(get_player_list());
        yield return StartCoroutine(get_current_consumption());
    }

    // Coroutine to retrieve player list data from API
    private IEnumerator get_player_list()
    {
        Debug.Log("Getting player list");
        yield return StartCoroutine(APIHubScript.Instance.get_request(ViewPlayerList_API));
        Debug.Log(APIHubScript.Instance.Response);

        if (APIHubScript.Instance.Response != null)
        {
            data = APIHubScript.Instance.Response;
            playerlist = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            Debug.Log(data);
        }
        else
        {
            Debug.LogError("Token response is null.");
        }
    }

    // Coroutine to retrieve current consumption data from API
    private IEnumerator get_current_consumption()
    {
        Debug.Log("Getting player consumption");
        yield return StartCoroutine(APIHubScript.Instance.get_request(ViewCurrentUnits_API));
        Debug.Log(APIHubScript.Instance.Response);

        if (APIHubScript.Instance.Response != null)
        {
            units = APIHubScript.Instance.Response;
            consumption = JsonConvert.DeserializeObject<Dictionary<string, double>>(units);
            Current_units = consumption["currentConsumption"];
            Debug.Log("CurrentConsumption :" + consumption["currentConsumption"]);

            // Update UI with current coins and calculate scores
            currentCoins = coinManagerScript.Instance.getCoins();
            Coins.text = currentCoins.ToString("F1"); 
            calculate_Scores();
        }
        else
        {
            Debug.LogError("Token response is null.");
        }
    }

    // Coroutine to retrieve player profile data from API
    private IEnumerator get_player_profile()
    {
        Debug.Log("Getting player profile");
        yield return StartCoroutine(APIHubScript.Instance.get_request(ViewProfile_API));
        Debug.Log(APIHubScript.Instance.Response);

        if (APIHubScript.Instance.Response != null)
        {
            playerData = JsonUtility.FromJson<PlayerData>(APIHubScript.Instance.Response);
            Debug.Log("Player Data :" + playerData);
            Myusername = playerData.user.username;
            Debug.Log("My user name :" + Myusername);
        }
        else
        {
            Debug.LogError("Token response is null.");
        }
    }

    // Method to calculate scores based on consumption data
    public void calculate_Scores()
    {
        int constant = 30;
        Double factor = Current_units / ((1 + new System.Random().NextDouble()) * constant);

        foreach (var outerPair in playerlist)
        {
            var players = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(outerPair.Value.ToString());
            foreach (var player in players)
            {
                if (player.TryGetValue("username", out object usernameObj) && usernameObj is string username)
                {
                    consumption_dic[username] = ((1 + new System.Random().NextDouble()) * constant) * factor;
                }
            }
            consumption_dic[Myusername] = Current_units;
        }

        foreach (var entry in consumption_dic)
        {
            Debug.Log("Username: " + entry.Key + ", consumption: " + entry.Value);
        }
        Scoring();
    }

    // Method to calculate scores and update leaderboard UI
    public void Scoring()
    {
        double myScore;

        foreach (var comp in consumption_dic)
        {
            score_dic[comp.Key] = Scoring_Strategy((float)comp.Value);
        }
        score_dic[Myusername] += currentCoins / 10;
        Debug.Log("My score" + score_dic[Myusername]);
        myScore = score_dic[Myusername];

        // Convert dictionary to list of key-value pairs and sort by score
        List<KeyValuePair<string, double>> sortedList = new List<KeyValuePair<string, double>>(score_dic);
        sortedList.Sort((x, y) => y.Value.CompareTo(x.Value));

        // Iterate through the list to find the position of the key
        for (int i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i].Key == Myusername)
            {
                MyRank = i; // Found the position
                Debug.Log("MyRank is :" + MyRank);
                break; // Exit the loop since we found the key
            }
            Debug.Log("Not found My user name");
            MyRank = -1;
        }

        // Display top 10 scores and highlight user's rank
        int numLabels = Mathf.Min(10, sortedList.Count);
        for (int i = 0; i < numLabels; i++)
        {
            text_labels[i][1].text = sortedList[i].Value.ToString("F2"); // Set score text
            text_labels[i][2].text = sortedList[i].Key; // Set username text
        }
        for (int i = numLabels; i < 10; i++)
        {
            text_labels[i][1].text = "x"; // Set score text
            text_labels[i][2].text = "x"; // Set username text
        }

        // Display comment based on user's rank
        if (MyRank == 0)
        {
            comment.text = "Wow! You're #1! Keep saving energy to stay on top!";
        }
        else if (MyRank == 1)
        {
            comment.text = "Wow! You're #2! Save more energy to claim the top spot!";
        }
        else if (MyRank == 2)
        {
            comment.text = "Wow! You're #3! Keep conserving to climb even higher!";
        }
        else if (MyRank >= 3 && MyRank <= 9)
        {
            comment.text = $"You're in the top {numLabels}! Keep saving energy to stay ahead!";
        }
        else
        {
            comment.text = $"Your score: {myScore:F2} & #{MyRank + 1}. You're below the top 10. Save more energy to boost your rank!";
        }

        // Highlight user's rank in the leaderboard UI
        if (MyRank < text_labels.Count && MyRank >= 0)
        {
            text_labels[MyRank][0].color = new Color(0.5f, 0f, 0.5f);
            text_labels[MyRank][1].color = new Color(0.5f, 0f, 0.5f);
            text_labels[MyRank][2].color = new Color(0.5f, 0f, 0.5f);
        }
    }

    // Method to apply scoring strategy based on consumption
    public float Scoring_Strategy(float consumption)
    {
        if (consumption < 10000) { return 500 * Mathf.Exp(-consumption / 100000); }
        else if (consumption < 20000) { return 500 * Mathf.Exp(-consumption / 75000); }
        else if (consumption < 30000) { return 500 * Mathf.Exp(-consumption / 65000); }
        else if (consumption < 40000) { return 500 * Mathf.Exp(-consumption / 40000); }
        else { return 500 * Mathf.Exp(-consumption / 30000); }
    }

    //initializing UI
    private void initialRanking()
    {
        text_labels = new List<List<TMP_Text>>
        {
            new List<TMP_Text> { Rank1, Score1, userName1 },
            new List<TMP_Text> { Rank2, Score2, userName2 },
            new List<TMP_Text> { Rank3, Score3, userName3 },
            new List<TMP_Text> { Rank4, Score4, userName4 },
            new List<TMP_Text> { Rank5, Score5, userName5 },
            new List<TMP_Text> { Rank6, Score6, userName6 },
            new List<TMP_Text> { Rank7, Score7, userName7 },
            new List<TMP_Text> { Rank8, Score8, userName8 },
            new List<TMP_Text> { Rank9, Score9, userName9 },
            new List<TMP_Text> { Rank10, Score10, userName10 }
        };

        for (int i = 0; i < 10; i++)
        {
            text_labels[i][1].text = "Loading..."; // Set score text
            text_labels[i][2].text = "Loading..."; // Set username text
        }
    }

}
