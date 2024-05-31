using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class foodStallScript : MonoBehaviour
{
    public GameObject confirmPopup;
    public GameObject insufficientCoinsPopup;
    // public coinManagerScript coinManager;
    public inventoryScript.StallLevel stallLevel;
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI levelPrice;
    public TextMeshProUGUI balance;
    public TextMeshProUGUI collectedCoins;
    public TextMeshProUGUI levelDescriptor;
    private float level1Prize = 600;
    private float level2Prize = 1200;
    private float level3Prize = 1800;
    private float currentLevelPrice;
    private int currentLevelId = 0;

    void Start() {
        coinManagerScript.Instance.calculate_stall_coins(stallLevel.level+2);
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
        // dicrease the coin
        coinManagerScript.Instance.removeCoins(currentLevelPrice);
        // add the animal to the inventory
        stallLevel.addLevel(currentLevelId);
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

    private void show_collected_coins() {
        coinManagerScript.Instance.calculate_stall_coins(stallLevel.level+2);
        float stallCoins = coinManagerScript.Instance.stall_coins;
        collectedCoins.text = stallCoins.ToString("F2");
    }
}
