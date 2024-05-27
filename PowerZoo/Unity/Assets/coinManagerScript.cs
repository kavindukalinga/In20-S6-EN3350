using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class coinManagerScript : MonoBehaviour
{
    private float coins = 500;
    private float previousCurrentUnits = 0.0f;
    public APIHubScript APIHub;

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
        yield return StartCoroutine(APIHub.get_coins());
        yield return StartCoroutine(APIHub.get_daily_power());
        yield return StartCoroutine(APIHub.get_current_units());
        coins = APIHub.coinResponse.coins;
        Dictionary<string, float> dailyConsumed = APIHub.dailyPower.dailyPowerConsumptionView.dailyUnits;
        float currentUnits = APIHub.currentUnits.currentConsumption;
        if (previousCurrentUnits != 0.0) {
            float current_rate = currentUnits - previousCurrentUnits;
            float avg_units = take_avg_from_dict(dailyConsumed);
            float normal_rate = take_avg_rate_for_10secs(avg_units);
            if (current_rate > normal_rate) {
                coins -= (float)Math.Pow(10.0, (current_rate - normal_rate));
            } else {
                coins += (float)Math.Pow(20.0, (current_rate - normal_rate));
            }
        }
    }

    private float take_avg_from_dict(Dictionary<string, float> dailyUnits) {
        float sum = 0;
        foreach (KeyValuePair<string, float> entry in dailyUnits) {
            sum += entry.Value;
        }
        return sum / dailyUnits.Count;
    }

    private float take_avg_rate_for_10secs(float avg_units) {
        float avg_units_wh = avg_units * 1000;
        float avg_rate = avg_units_wh / (360*24);
        return avg_rate;
    }
}
