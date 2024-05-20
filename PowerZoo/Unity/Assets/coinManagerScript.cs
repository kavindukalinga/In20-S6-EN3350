using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinManagerScript : MonoBehaviour
{
    private int coins = 500;

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
}
