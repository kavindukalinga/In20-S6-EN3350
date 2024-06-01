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
    public TextMeshProUGUI collectedCoins;
    public TextMeshProUGUI levelDescriptor;
    private int stall_num;
    private float level1Prize = 600;
    private float level2Prize = 1200;
    private float level3Prize = 1800;
    private float currentLevelPrice;
    private int currentLevelId = 0;

    void Start() {
        stall_num = activeStallName.activeStall
        int coins_per_hour;
        int current_level;
        // yield return StartCoroutine
        if (stall_num == 1) {coins_per_hour = stallLevel.stall1Level + 2}
        else if (stall_num == 2) {coins_per_hour = stallLevel.stall2Level + 2}
        else if (stall_num == 3) {coins_per_hour = stallLevel.stall3Level + 2}
        
        // Debug.Log("Stall Level: ");
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
        // dicrease the coin
        coinManagerScript.Instance.removeCoins(currentLevelPrice);
        // add the animal to the inventory
        if (stall_num == 1) {stallLevel.stall1Level + 1}
        else if (stall_num == 2) {stallLevel.stall2Level + 1}
        else if (stall_num == 3) {stallLevel.stall3Level + 1}
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
        coinManagerScript.Instance.stall_coins = 0;
        collectedCoins.text = "0";
    }
}
