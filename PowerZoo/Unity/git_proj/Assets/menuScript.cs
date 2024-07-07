using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class menuScript : MonoBehaviour
{
    // public Button playerProfileButton;
    // public Button playerProfile2Button;
    // public APIHubScript APIHub;
    public TMP_Text coinsText;
    private int coins;
    
    IEnumerator Start()
    {
        // playerProfileButton.onClick.AddListener(load_player_profile);
        // playerProfile2Button.onClick.AddListener(load_player_profile);
        // initialize_coins();
        yield return StartCoroutine(coinManagerScript.Instance.initializeCoins());
        yield return StartCoroutine(coinManagerScript.Instance.calculate_offline_coins());
    }

    public void load_player_profile() {
        SceneManager.LoadScene("PlayerProfileScene");
    }

    private void initialize_coins() {
        StartCoroutine(coinManagerScript.Instance.initializeCoins());
    }

    public void load_game() {
        SceneManager.LoadScene("zooenv");
    }

    public void go_to_leaderboard()
    {
        SceneManager.LoadScene("leaderboard");
    }

    // private IEnumerator calculate_coins() {
    //     yield return StartCoroutine(APIHub.get_score());
    //     coins = APIHub.scoreResponse.score * 20;
    //     coinsText.text = coins.ToString();
    // }
}
