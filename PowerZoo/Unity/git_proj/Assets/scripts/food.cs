using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;


public class Food : MonoBehaviour
{
    // Variables for the Text Assets
    public Text Food1;
    public Text Food2;
    public Text Food3;
    public Text count1;
    public Text count2;
    public Text count3;
    public Text hp1;
    public Text hp2;
    public Text hp3;

    // Variables for the Image Sprites
    public Sprite elephant;
    public Sprite giraffe;
    public Sprite lion;
    public Sprite tiger;
    public Sprite zebra;
    public GameObject AnimalBoard;
    public GameObject health;

    // Variables for the Buttons
    public Button feed1;
    public Button feed2;
    public Button feed3;

    public int healthValue;
    private List<FoodData> currentFoodList;
    private Button[] feedButtons;

    [Serializable]
    private class AnimalHealthUpdate
    {
        public int index;
        public int delta;
    }

    [Serializable]
    public class FoodData
    {
        public string name;
        public int count;
        public int healthPoints;
    }

    [Serializable]
    public class FoodList
    {
        public List<FoodData> foods;
    }

    [Serializable]
    public class HealthList
    {
        public List<int> healthData;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T foods;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the game object
        string animal = staticDataStore.staticData;
        int index = staticDataStore.index;
        UpdateAnimalSprite(animal);

        // Start the coroutine to fetch and display food data
        StartCoroutine(FetchFoodData(animal));
        StartCoroutine(FetchHealthData(animal));

        // Initialize feed buttons array
        feedButtons = new Button[] { feed1, feed2, feed3 };

        // Adding listeners to buttons
        feed1.onClick.AddListener(() => OnFeedButtonClick(0, animal));
        feed2.onClick.AddListener(() => OnFeedButtonClick(1, animal));
        feed3.onClick.AddListener(() => OnFeedButtonClick(2, animal));
    }

    void UpdateAnimalSprite(string animal)
    {
        Sprite newSprite = null;
        switch (animal.ToLower())
        {
            case "elephant":
                newSprite = elephant;
                break;
            case "giraffe":
                newSprite = giraffe;
                break;
            case "lion":
                newSprite = lion;
                break;
            case "tiger":
                newSprite = tiger;
                break;
            case "zebra":
                newSprite = zebra;
                break;
            default:
                UnityEngine.Debug.LogError("Unknown animal type: " + animal);
                break;
        }

        if (AnimalBoard != null && newSprite != null)
        {
            // Assuming AnimalBoard has a SpriteRenderer (for 2D objects)
            SpriteRenderer spriteRenderer = AnimalBoard.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                // If AnimalBoard has an Image component (for UI elements)
                Image image = AnimalBoard.GetComponent<Image>();
                if (image != null)
                {
                    image.sprite = newSprite;
                }
                else
                {
                    UnityEngine.Debug.LogError("AnimalBoard does not have a SpriteRenderer or Image component.");
                }
            }
        }
    }

    void OnFeedButtonClick(int buttonIndex, string animalType)
    {
        UnityEngine.Debug.Log("Feed button clicked: " + buttonIndex);
        if (currentFoodList != null && buttonIndex < currentFoodList.Count)
        {
            FoodData selectedFood = currentFoodList[buttonIndex];
            UnityEngine.Debug.Log("buttonIndex : " + buttonIndex);
            UnityEngine.Debug.Log("helathpoint :" + selectedFood.healthPoints);
            float scale = 1; // Default scale
            switch (animalType.ToLower())
            {
                case "elephant":
                    scale = 20000;
                    break;
                case "giraffe":
                    scale = 14000;
                    break;
                case "lion":
                    scale = 12000;
                    break;
                case "tiger":
                    scale = 11000;
                    break;
                case "zebra":
                    scale = 5000;
                    break;
                default:
                    UnityEngine.Debug.LogError("Unknown animal type: " + animalType);
                    break;
            }

            if (healthValue + selectedFood.healthPoints > scale)
            {
                selectedFood.healthPoints = (int)scale - healthValue;
            }
            if (buttonIndex == 2)
            {
                selectedFood.healthPoints = (int)scale - healthValue;
            }

            healthValue = healthValue + selectedFood.healthPoints;
            float healthScale = Mathf.Clamp01(((float)healthValue) / scale);
            UnityEngine.Debug.Log("healthScale " + healthScale);
            health.transform.localScale = new Vector3(healthScale, health.transform.localScale.y, health.transform.localScale.z);

            if (healthValue == (int)scale)
            {
                feedButtons[buttonIndex].interactable = false;
            }

            StartCoroutine(PostAnimalHealth(buttonIndex, selectedFood.healthPoints));
            StartCoroutine(UpdateFoodCount(selectedFood.name, -1, buttonIndex));
        }
    }

    IEnumerator FetchFoodData(string animalType)
    {
        string url = $"http://localhost:9000/api/foods/{animalType.ToLower()}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to fetch food data: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            FoodList foodList = JsonUtility.FromJson<FoodList>("{\"foods\":" + jsonResponse + "}");
            currentFoodList = foodList.foods;
            DisplayFoodData(foodList.foods, animalType);
        }
    }

    IEnumerator FetchHealthData(string animalType)
    {
        string url = $"http://localhost:9000/api/animals/healths/{animalType.ToLower()}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to fetch health data: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            HealthList healthList = JsonUtility.FromJson<HealthList>("{\"healthData\":" + jsonResponse + "}");

            // Log the entire healthList and its count
            UnityEngine.Debug.Log("HealthList: " + string.Join(", ", healthList.healthData));
            UnityEngine.Debug.Log("HealthList Count: " + healthList.healthData.Count);

            int index = staticDataStore.index;
            UnityEngine.Debug.Log("Index: " + index);

            if (index >= 0 && index < healthList.healthData.Count)
            {
                healthValue = healthList.healthData[index];

                float scale = 1; // Default scale
                switch (animalType.ToLower())
                {
                    case "elephant":
                        scale = 20000;
                        break;
                    case "giraffe":
                        scale = 14000;
                        break;
                    case "lion":
                        scale = 12000;
                        break;
                    case "tiger":
                        scale = 11000;
                        break;
                    case "zebra":
                        scale = 5000;
                        break;
                    default:
                        UnityEngine.Debug.LogError("Unknown animal type: " + animalType);
                        break;
                }

                float healthScale = Mathf.Clamp01((float)healthValue / scale);
                health.transform.localScale = new Vector3(healthScale, health.transform.localScale.y, health.transform.localScale.z);
            }
            else
            {
                UnityEngine.Debug.LogError("Index out of range for health list.");
            }
        }
    }

    IEnumerator PostAnimalHealth(int index, int delta)
    {
        int animalIndex = staticDataStore.index;
        AnimalHealthUpdate update = new AnimalHealthUpdate { index = animalIndex, delta = delta };
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
            UnityEngine.Debug.Log("Successfully posted animal health.");
        }
    }

    IEnumerator UpdateFoodCount(string foodName, int value, int buttonIndex)
    {
        string url = $"http://localhost:9000/api/foods/{foodName}?value={value}";
        UnityWebRequest request = UnityWebRequest.Put(url, "");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to update food count: " + request.error);
        }
        else
        {
            UnityEngine.Debug.Log("Successfully updated food count.");

            // Update local food count and UI
            currentFoodList[buttonIndex].count += value;
            if (currentFoodList[buttonIndex].count <= 0)
            {
                // Disable the feed button if the food count is zero
                feedButtons[buttonIndex].interactable = false;
            }

            switch (buttonIndex)
            {
                case 0:
                    count1.text = currentFoodList[buttonIndex].count.ToString();
                    break;
                case 1:
                    count2.text = currentFoodList[buttonIndex].count.ToString();
                    break;
                case 2:
                    count3.text = currentFoodList[buttonIndex].count.ToString();
                    break;
            }
        }
    }

    void DisplayFoodData(List<FoodData> foodList, string animalType)
    {
        if (foodList.Count > 0)
        {
            Food1.text = foodList[0].name;
            count1.text = foodList[0].count.ToString();
            hp1.text = foodList[0].healthPoints.ToString() + " hp";
            if (foodList[0].count == 0)
            {
                feed1.interactable = false;
            }
        }

        if (foodList.Count > 1)
        {
            Food2.text = foodList[1].name;
            count2.text = foodList[1].count.ToString();
            hp2.text = foodList[1].healthPoints.ToString() + " hp";
            if (foodList[1].count == 0)
            {
                feed2.interactable = false;
            }
        }

        if (foodList.Count > 2)
        {
            Food3.text = foodList[2].name;
            count3.text = foodList[2].count.ToString();
            hp3.text = "unlimited";
            if (foodList[2].count == 0)
            {
                feed3.interactable = false;
            }
        }
    }
}
