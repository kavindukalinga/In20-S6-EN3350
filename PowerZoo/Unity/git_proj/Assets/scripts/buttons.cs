using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Diagnostics;

public class buttons : MonoBehaviour
{
    public Button feed1;
    public Button feed2;
    public Button feed3;
    public Button sell1;
    public Button sell2;
    public Button sell3;

    private Button[] feedButtons;
    private Button[] sellButtons;

    // Class to hold the request body data
    [System.Serializable]
    private class AnimalHealthUpdate
    {
        public int index;
        public int delta;
    }

    [System.Serializable]
    private class CoinsResponse
    {
        public int coins;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the buttons array
        feedButtons = new Button[] { feed1, feed2, feed3 };
        sellButtons = new Button[] { sell1, sell2, sell3 };

        // Assign onClick listeners to the buttons
        feed1.onClick.AddListener(() => OnFeedButtonClick(0));
        feed2.onClick.AddListener(() => OnFeedButtonClick(1));
        feed3.onClick.AddListener(() => OnFeedButtonClick(2));
        sell1.onClick.AddListener(() => OnSellButtonClick(0));
        sell2.onClick.AddListener(() => OnSellButtonClick(1));
        sell3.onClick.AddListener(() => OnSellButtonClick(2));
    }

    // Method to handle feed button click
    void OnFeedButtonClick(int index)
    {
        staticDataStore.index = index;
        // Load the new scene
        SceneManager.LoadScene("food"); // Replace "food" with the actual name of the scene you want to load
    }

    

    // Method to handle sell button click
    void OnSellButtonClick(int index)
    {
        staticDataStore.index = index;
        string animalType = staticDataStore.staticData.ToLower();

        int coins = GetCoinsBasedOnAnimalType(animalType);
        StartCoroutine(SellAnimal(index, coins));
    }

    int GetCoinsBasedOnAnimalType(string animalType)
    {
        switch (animalType)
        {
            case "elephant":
                return 200; // example value
            case "giraffe":
                return 100; // example value
            case "lion":
                return 180; // example value
            case "tiger":
                return 120; // example value
            case "zebra":
                return 80; // example value
            default:
                return 100; // default value
        }
    }

    IEnumerator SellAnimal(int index, int coins)
    {
        string getCoinurl = "http://localhost:9000/api/players/coins";
        UnityWebRequest coinsRequest = UnityWebRequest.Get(getCoinurl);
        yield return coinsRequest.SendWebRequest();

        if (coinsRequest.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to get coins: " + coinsRequest.error);
            yield break;
        }

        CoinsResponse coinsResponse = JsonUtility.FromJson<CoinsResponse>(coinsRequest.downloadHandler.text);
        int totalCoins = coinsResponse.coins + coins;
        //coinManagerScript.Instance.addCoins((float)coins);
        string url = $"http://localhost:9000/api/players/coins?coins={totalCoins}";
        UnityWebRequest request = UnityWebRequest.Put(url, "");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to update coins: " + request.error);
        }
        else
        {
            string healthUrl = $"http://localhost:9000/api/animals/healths/{staticDataStore.staticData.ToLower()}";
            UnityWebRequest healthRequest = UnityWebRequest.Get(healthUrl);
            yield return healthRequest.SendWebRequest();

            if (healthRequest.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError("Failed to fetch health data: " + healthRequest.error);
            }
            else
            {
                string jsonResponse = healthRequest.downloadHandler.text;
                List<int> healthList = JsonUtility.FromJson<Wrapper<List<int>>>("{\"data\":" + jsonResponse + "}").data;
                int animalHealth = healthList[index];

                AnimalHealthUpdate update = new AnimalHealthUpdate { index = index, delta = -animalHealth };
                string jsonBody = JsonUtility.ToJson(update);
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

                UnityEngine.Debug.Log("Sending JSON body: " + jsonBody);
                UnityEngine.Debug.Log("Sending byte array: " + System.BitConverter.ToString(bodyRaw));

                string updateUrl = $"http://localhost:9000/api/animals/healths/{staticDataStore.staticData.ToLower()}";
                UnityWebRequest updateRequest = new UnityWebRequest(updateUrl, "POST");
                updateRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                updateRequest.downloadHandler = new DownloadHandlerBuffer();
                updateRequest.SetRequestHeader("Content-Type", "application/json");
                yield return updateRequest.SendWebRequest();

                if (updateRequest.result != UnityWebRequest.Result.Success)
                {
                    UnityEngine.Debug.LogError("Failed to update animal health: " + updateRequest.error);
                }
                else
                {
                    // Deactivate the sell and feed buttons
                    feedButtons[index].interactable = false;
                    sellButtons[index].interactable = false;

                   
                    
   
                }
            }
        }
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T data;
    }
}
