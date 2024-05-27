using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinManagerScript : MonoBehaviour
{
    private int coins = 500;
    public APIHubScript APIHub;

    public int getCoins()
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

    public void incrementCoins()
    {
        yield return StartCoroutine(APIHub.get_coins());
        yield return StartCoroutine(APIHub.get_coins());
        APIHub.coins += 1;
    }
}
