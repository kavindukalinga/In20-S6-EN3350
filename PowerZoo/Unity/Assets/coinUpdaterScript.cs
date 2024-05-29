using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class coinUpdaterScript : MonoBehaviour
{
    private TextMeshProUGUI balance;
    public bool isSceneNeedUpdateCoins = true;
    // private coinManagerScript coinManager;

    void Start()
    {
        // coinManager = GameObject.Find("CoinManagerScript").GetComponent<coinManagerScript>();
        balance = GameObject.Find("Canvas/Coins/Balance").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (isSceneNeedUpdateCoins)
        {
            float coins = coinManagerScript.Instance.getCoins();
            balance.text = coins.ToString("F2");
        }
    }
}
