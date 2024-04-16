using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class menuScript : MonoBehaviour
{
    public Button playerProfileButton;
    public Button playerProfile2Button;
    public APIHubScript APIHub;
    public TMP_Text coinsText;
    private int coins;
    // Start is called before the first frame update
    void Start()
    {
        playerProfileButton.onClick.AddListener(load_player_profile);
        playerProfile2Button.onClick.AddListener(load_player_profile);
        update_coins();
    }

    private void load_player_profile() {
        SceneManager.LoadScene("PlayerProfile");
    }

    private void update_coins() {
        StartCoroutine(calculate_coins());
    }

    private IEnumerator calculate_coins() {
        yield return StartCoroutine(APIHub.get_score());
        coins = APIHub.scoreResponse.score * 20;
        coinsText.text = coins.ToString();
    }
}
