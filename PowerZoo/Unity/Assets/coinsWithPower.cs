using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinsWithPower : MonoBehaviour
{
    // private coinManagerScript coinManager;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // coinManager = GameObject.Find("CoinManagerScript").GetComponent<coinManagerScript>();
        yield return StartCoroutine(coinManagerScript.Instance.take_avg_units());
        yield return StartCoroutine(updateCoinsWithPower());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator updateCoinsWithPower()
    {
        while (true)
        {
            yield return StartCoroutine(coinManagerScript.Instance.incrementCoins());
            yield return new WaitForSeconds(10);
        }
    }
}
