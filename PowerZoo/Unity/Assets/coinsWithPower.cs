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
        yield return new WaitForSeconds(10);
        yield return StartCoroutine(coinManagerScript.Instance.take_avg_units());
        yield return StartCoroutine(updateCoinsWithPower());
    }

    private IEnumerator updateCoinsWithPower()
    {
        int count = 0;
        while (true)
        {
            yield return StartCoroutine(coinManagerScript.Instance.incrementCoins());
            yield return new WaitForSeconds(10);
            count++;
            if (count == 6)
            {
                yield return StartCoroutine(coinManagerScript.Instance.put_coins_to_backend());
                yield return StartCoroutine(APIHubScript.Instance.put_last_logging());
                count = 0;
            }
        }
    }
}
