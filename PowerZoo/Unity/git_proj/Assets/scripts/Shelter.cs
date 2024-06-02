using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[System.Serializable]
public class Animal
{
    public string name;
    public int health;
}


public class Shelter : MonoBehaviour
{
    // Variables for the Text Assets
    public Text NameBoard;
    public Text Animal1;
    public Text Animal2;
    public Text Animal3;

    // Variables for the Image Sprites
    public Sprite elephant;
    public Sprite giraffe;
    public Sprite lion;
    public Sprite tiger;
    public Sprite zebra;
    public GameObject AnimalBoard;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    // Variables for the Buttons
    public Button feed1;
    public Button feed2;
    public Button feed3;
    public Button sell1;
    public Button sell2;
    public Button sell3;

    // JSON string for testing
    
    private Dictionary<string, List<Animal>> animalsDictionary;
    private int selectedIndex;

    // Start is called before the first frame update
    void Start()
    {
        string animal = staticDataStore.staticData;
        NameBoard.text = animal;
        UpdateAnimalSprite(animal);
        StartCoroutine(InitializeAnimals(animal));
    }

    IEnumerator InitializeAnimals(string animal)
    {
        animalsDictionary = new Dictionary<string, List<Animal>>();

        // Fetch health values from the API
        string url = $"http://localhost:9000/api/animals/healths/{animal.ToLower()}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to fetch health data: " + request.error);
        }
        else
        {
            // Log the raw response for debugging
            string jsonResponse = request.downloadHandler.text;
            UnityEngine.Debug.Log("JSON Response: " + jsonResponse);

            // Deserialize the JSON response
            List<int> healthList = JsonUtility.FromJson<Wrapper<List<int>>>("{\"data\":" + jsonResponse + "}").data;

            // Log the deserialized health list for debugging
            UnityEngine.Debug.Log("Deserialized Health List: " + string.Join(", ", healthList));

            animalsDictionary[animal.ToLower()] = CreateAnimalsFromHealthList(healthList);
        }

        UpdateAnimalNames(animal);
        UpdateHealthSprites(animal);
        StartCoroutine(DecreaseHealthOverTime());
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T data;
    }

    List<Animal> CreateAnimalsFromHealthList(List<int> healthList)
    {
        List<Animal> animals = new List<Animal>();
        for (int i = 0; i < healthList.Count; i++)
        {
            animals.Add(new Animal { name = "Animal" + (i + 1), health = healthList[i] });
        }
        return animals;
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

    void UpdateAnimalNames(string animalType)
    {
        if (animalsDictionary.TryGetValue(animalType.ToLower(), out var animals) && animals != null)
        {
            for (int i = 0; i < 3; i++)
            {
                Text animalText = null;
                Button feedButton = null;
                Button sellButton = null;

                // Assign the text field and buttons based on the index
                switch (i)
                {
                    case 0:
                        animalText = Animal1;
                        feedButton = feed1;
                        sellButton = sell1;
                        break;
                    case 1:
                        animalText = Animal2;
                        feedButton = feed2;
                        sellButton = sell2;
                        break;
                    case 2:
                        animalText = Animal3;
                        feedButton = feed3;
                        sellButton = sell3;
                        break;
                }

                if (i < animals.Count && animals[i].health > 0)
                {
                    animalText.text = animals[i].name;
                    feedButton.interactable = true;
                    sellButton.interactable = true;
                }
                else
                {
                    animalText.text = "";
                    feedButton.interactable = false;
                    sellButton.interactable = false;
                }
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Unknown animal type: " + animalType);
        }
    }

    void UpdateHealthSprites(string animalType)
    {
        if (animalsDictionary.TryGetValue(animalType.ToLower(), out var animals) && animals != null)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject healthObject = null;

                // Assign the health object based on the index
                switch (i)
                {
                    case 0:
                        healthObject = health1;
                        break;
                    case 1:
                        healthObject = health2;
                        break;
                    case 2:
                        healthObject = health3;
                        break;
                }

                float scale = 0;

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
                    
                }

                if (i < animals.Count)
                {
                    float healthScale = Mathf.Clamp01(animals[i].health / scale);
                    healthObject.transform.localScale = new Vector3(healthScale, healthObject.transform.localScale.y, healthObject.transform.localScale.z);
                }
                else
                {
                    healthObject.transform.localScale = new Vector3(0, healthObject.transform.localScale.y, healthObject.transform.localScale.z);
                }
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Animal data is null.");
        }
    }

    IEnumerator DecreaseHealthOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(60.0f);

            foreach (var animalList in animalsDictionary.Values)
            {
                DecreaseAnimalHealth(animalList);
            }

            UpdateHealthSprites(NameBoard.text);
        }
    }

    void DecreaseAnimalHealth(List<Animal> animals)
    {
        foreach (var animal in animals)
        {
            animal.health -= 1;
            if (animal.health < 0)
                animal.health = 0;
        }
    }


}
