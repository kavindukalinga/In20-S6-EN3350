using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class coinManagerScript : MonoBehaviour
{
    public float coins = 500;
    public float previousCurrentUnits = 0.0f;
    public float avg_units = 0.0f;
    public float stall_coins;
    private float avg_rate = 0.0f;
    private float increment_rate = 1.18f;
    private float decrement_rate = 1.05f;
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

    public void addCoins(float amount)
    {
        coins += amount;
    }

    public void removeCoins(float amount)
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
                coins -= ((float)Math.Pow(decrement_rate, (current_rate - avg_rate))-1);
            } else {
                coins += ((float)Math.Pow(increment_rate, (avg_rate - current_rate))-1);
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

    public void calculate_offline_coins() {
        // yield return StartCoroutine(APIHubScript.Instance.get_last_logging())
        // DateTime last_logging = APIHubScript.Instance.lastLogging.lastLoggingTime;
        DateTime last_logging = new DateTime(2024, 5, 28);
        DateTime current_logging = DateTime.Now;
        TimeSpan diff = current_logging - last_logging;
        int diff_in_days = diff.Days;
        for (DateTime date = last_logging; date <= current_logging; date = date.AddDays(1))
        {
            // yield return StartCoroutine(APIHubScript.Instance.get_daily_power(date));
            Dictionary<string, float> dailyConsumed = APIHubScript.Instance.dailyPower.dailyPowerConsumptionView.dailyUnits;
            int day = date.Day;
            
            float power_of_day = dailyConsumed[day.ToString()];
            if (power_of_day > avg_units) {
                coins -= ((float)Math.Pow(decrement_rate, (power_of_day - avg_units)*0.11574)-1)*8640; // 0.11574 = 1000/8640; (8640 is number of 10secs in a day)
            } else {
                coins += ((float)Math.Pow(increment_rate, (avg_units - power_of_day)*0.11574)-1)*8640;
            }
            Debug.Log("Day: " + coins);
        }
    }

    public void calculate_stall_coins(float coins_per_hour) {
        // yield return StartCoroutine(APIHubScript.Instance.get_last_logging())
        // DateTime last_logging = APIHubScript.Instance.lastLogging.lastLoggingTime;
        DateTime last_logging = new DateTime(2024, 5, 28);
        DateTime current_logging = DateTime.Now;
        TimeSpan diff = current_logging - last_logging;
        int diff_in_hours = diff.Hours;
        stall_coins = coins_per_hour * diff_in_hours;
    }
}