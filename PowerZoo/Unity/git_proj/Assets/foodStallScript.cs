using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class foodStallScript : MonoBehaviour
{
    public GameObject confirmPopup;
    public GameObject insufficientCoinsPopup;
    public inventoryScript.StallLevel stallLevel;
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI levelPrice;
    public TextMeshProUGUI collectedCoins;
    public TextMeshProUGUI levelDescriptor;
    public TextMeshProUGUI levelText;
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    private int stall_num;
    private float level1Prize = 600;
    private float level2Prize = 1200;
    private float level3Prize = 1800;
    private float currentLevelPrice;
    private int currentLevelId = 0;
    public int current_level = 0;

    IEnumerator Start() {
        stall_num = activeStallName.activeStall;
        int coins_per_hour;
        yield return StartCoroutine(APIHubScript.Instance.get_stall_level(stall_num));
        current_level = APIHubScript.Instance.stallLevel;
        coins_per_hour = current_level + 2;
        
        if (current_level == 0) {
            level1Button.interactable = true;
            level2Button.interactable = false;
            level3Button.interactable = false;
        } else if (current_level == 1) {
            level1Button.interactable = false;
            level2Button.interactable = true;
            level3Button.interactable = false;
        } else if (current_level == 2) {
            level1Button.interactable = false;
            level2Button.interactable = false;
            level3Button.interactable = true;
        } else if (current_level == 3) {
            level1Button.interactable = false;
            level2Button.interactable = false;
            level3Button.interactable = false;
        }
        
        string level_text = "Stall " + stall_num.ToString() + "    Level " + current_level.ToString();
        Debug.Log(level_text);
        levelText.text = level_text;
        
        show_collected_coins(coins_per_hour);
    }


    public void showPopup(int level)
    {
        string popupText = "";
        string levelDescription = "";
        if (level == 1)
        {
            currentLevelPrice = level1Prize;
            popupText = "Upgrade to Level 1?";
            levelDescription = "Food stall income rate increase from 2 to 3 coins per Hour";
        }
        else if (level == 2)
        {
            currentLevelPrice = level2Prize;
            popupText = "Upgrade to Level 2?";
            levelDescription = "Food stall income rate increase from 3 to 4 coins per Hour";
        }
        else if (level == 3)
        {
            currentLevelPrice = level3Prize;
            popupText = "Upgrade to Level 3?";
            levelDescription = "Food stall income rate increase from 4 to 5 coins per Hour";
        }

        if (coinManagerScript.Instance.getCoins() < currentLevelPrice) { // check if the player has enough coins
            showInsufficientCoinsPopup();
        } else { // show the purchase confirmation popup
            levelName.text = popupText;
            levelPrice.text = ((int)currentLevelPrice).ToString();
            levelDescriptor.text = levelDescription;
            currentLevelId = level;
            confirmPopup.SetActive(true);
        }
    }

    public void buyLevel()
    {
        Debug.Log("Buying level " + currentLevelId);
        // dicrease the coin
        coinManagerScript.Instance.removeCoins(currentLevelPrice);
        // add the animal to the inventory
        StartCoroutine(APIHubScript.Instance.put_stall_level(stall_num, currentLevelId));
        // stallLevel.addLevel(currentLevelId);
        confirmPopup.SetActive(false);
        SceneManager.LoadScene("zooenv");
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
        coinsText.text = currentLevelPrice.ToString();
        insufficientCoinsPopup.SetActive(true);
    }

    private void show_collected_coins(int coins_per_hour) {
        coinManagerScript.Instance.calculate_stall_coins(coins_per_hour);
        float stallCoins = coinManagerScript.Instance.stall_coins;
        collectedCoins.text = stallCoins.ToString("F2");
    }

    public void collectCoins() {
        coinManagerScript.Instance.addCoins(coinManagerScript.Instance.stall_coins);
        StartCoroutine(coinManagerScript.Instance.put_coins_to_backend());
        coinManagerScript.Instance.stall_coins = 0;
        collectedCoins.text = "0";
    }

    public void closeStall() {
        SceneManager.LoadScene("zooenv");
    }
}
