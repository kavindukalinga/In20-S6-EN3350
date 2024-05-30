using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class coinManagerScript : MonoBehaviour
{
    public float coins = 500;
    public float previousCurrentUnits = 0.0f;
    public float avg_units = 0.0f;
    private float avg_rate = 0.0f;
    // public APIHubScript APIHub;
    public static coinManagerScript Instance { get; private set; }

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


    public IEnumerator initializeCoins()
    {
        yield return StartCoroutine(APIHubScript.Instance.get_coins());
        coins = APIHubScript.Instance.coinResponse.coins;
    }

    public float getCoins()
    {
        return coins;
    }

    public void addCoins(int amount)
    {
        coins += amount;
    }

    public void removeCoins(int amount)
    {
        coins -= amount;
    }

    public IEnumerator incrementCoins()
    {
        // yield return StartCoroutine(APIHub.get_coins());
        yield return StartCoroutine(APIHubScript.Instance.get_current_units());
        // coins = APIHub.coinResponse.coins;
        float currentUnits = APIHubScript.Instance.currentUnits.currentConsumption;
        if (previousCurrentUnits != 0.0f) {
            float current_rate = currentUnits - previousCurrentUnits;
            // float avg_units = take_avg_from_dict(dailyConsumed);
            // float normal_rate = take_avg_rate_for_10secs(avg_units);
            if (current_rate > avg_rate) {
                coins -= ((float)Math.Pow(10.0, (current_rate - avg_rate))-1);
            } else {
                coins += ((float)Math.Pow(20.0, (avg_rate - current_rate))-1);
            }
            Debug.Log("Coins: " + coins);
            float temp = current_rate - avg_rate;
            Debug.Log("Rate: " + temp);
        }
        previousCurrentUnits = currentUnits;
    }

    public IEnumerator take_avg_units() {
        yield return StartCoroutine(APIHubScript.Instance.get_daily_power());
        Dictionary<string, float> dailyConsumed = APIHubScript.Instance.dailyPower.dailyPowerConsumptionView.dailyUnits;
        float sum = 0;
        foreach (KeyValuePair<string, float> entry in dailyConsumed) {
            sum += entry.Value;
        }
        avg_units =  sum / dailyConsumed.Count;
        take_avg_rate_for_10secs(avg_units);
    }

    private void take_avg_rate_for_10secs(float avg_units) {
        float avg_units_wh = avg_units * 1000;
        avg_rate = avg_units_wh / (360*24);
        // return avg_rate;
    }
}
