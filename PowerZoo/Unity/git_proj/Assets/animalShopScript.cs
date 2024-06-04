using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class animalShopScript : MonoBehaviour
{
    public GameObject confirmPopup;
    public GameObject insufficientCoinsPopup;
    // public coinManagerScript coinManager;
    public inventoryScript.Animals animals;
    public Button buyButton;
    public Button cancelButton;
    private Button elephantButton;
    private Button giraffButton;
    private Button lionButton;
    private Button tigerButton;
    private Button zebraButton;
    public TextMeshProUGUI animalName;
    public TextMeshProUGUI animalPrice;
    private int elephantPrize = 200;
    private int lionPrize = 180;
    private int tigerPrize = 120;
    private int giraffePrize = 100;
    private int zebraPrize = 80;
    private int currentAnimalId;
    private int currentAnimalPrice;

    IEnumerator Start()
    {
        elephantButton = GameObject.Find("Buy0").GetComponent<Button>();
        lionButton = GameObject.Find("Buy1").GetComponent<Button>();
        tigerButton = GameObject.Find("Buy2").GetComponent<Button>();
        giraffButton = GameObject.Find("Buy3").GetComponent<Button>();
        zebraButton = GameObject.Find("Buy4").GetComponent<Button>();

        yield return StartCoroutine(APIHubScript.Instance.get_animals());
        disable_buttons();
        buyButton.onClick.AddListener(buyAnimal);
        cancelButton.onClick.AddListener(cancelPurchase);
        elephantButton.onClick.AddListener(() => showPopup(0));
        lionButton.onClick.AddListener(() => showPopup(1));
        tigerButton.onClick.AddListener(() => showPopup(2));
        giraffButton.onClick.AddListener(() => showPopup(3));
        zebraButton.onClick.AddListener(() => showPopup(4));
    }

    private void disable_buttons() {
        AnimalsCount animalsCount = APIHubScript.Instance.animalsCount;
        if (animalsCount.elephant >= 3) {
            elephantButton.interactable = false;
        }
        if (animalsCount.lion >= 3) {
            lionButton.interactable = false;
        }
        if (animalsCount.tiger >= 3) {
            tigerButton.interactable = false;
        }
        if (animalsCount.giraffe >= 3) {
            giraffButton.interactable = false;
        }
        if (animalsCount.zebra >= 3) {
            zebraButton.interactable = false;
        }
    }

    private void showPopup(int animalId)
    {
        int[] popupPrices = new int[] {
            elephantPrize,
            lionPrize,
            tigerPrize,
            giraffePrize,
            zebraPrize
        };
        currentAnimalPrice = popupPrices[animalId];

        if (coinManagerScript.Instance.getCoins() < currentAnimalPrice) { // check if the player has enough coins
            showInsufficientCoinsPopup();
        } else { // show the purchase confirmation popup
            string[] popupTexts = new string[] {
                "Purchase an Elephant?",
                "Purchase a Lion?",
                "Purchase a Tiger?",
                "Purchase a Giraff?",
                "Purchase a Zebra?"
            };
            animalName.text = popupTexts[animalId];
            animalPrice.text = popupPrices[animalId].ToString();
            currentAnimalId = animalId;
            confirmPopup.SetActive(true);
        }
    }

    private void buyAnimal()
    {
        // dicrease the coin
        coinManagerScript.Instance.removeCoins(currentAnimalPrice);
        StartCoroutine(APIHubScript.Instance.add_animal(currentAnimalId));
        StartCoroutine(coinManagerScript.Instance.put_coins_to_backend());
        // add the animal to the inventory
        // animals.addAnimal(currentAnimalId);
        confirmPopup.SetActive(false);
    }

    public void cancelPurchase()
    {
        confirmPopup.SetActive(false);
        insufficientCoinsPopup.SetActive(false);
    }
    
    private void showInsufficientCoinsPopup()
    {
        float currentCoins = coinManagerScript.Instance.getCoins();
        TextMeshProUGUI coinsText = insufficientCoinsPopup.transform.Find("CurrentAmount").Find("NumCoins").GetComponent<TextMeshProUGUI>();
        coinsText.text = currentCoins.ToString();
        insufficientCoinsPopup.SetActive(true);
    }

    public void changeSchene() {
        SceneManager.LoadScene("zooenv");
    }
}