using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class loginScript : MonoBehaviour
{
    public APIHubScript APIHub;
    public GameObject loadingBG;
    public GameObject loginSuccessBG;
    public GameObject loginErrorBG;
    public GameObject textTMP;
    public sceneLoaderScript sceneLoader;

    void Start()
    {
        Authenticate();
    }

    void Update()
    {
        change_scene();
    }

    public void Authenticate() => StartCoroutine(change_background());

    public IEnumerator change_background() {
        background_select(true, false, false, false);
        yield return StartCoroutine(APIHub.player_authenticate());
        yield return StartCoroutine(APIHub.player_authenticate_spring());
        if (!string.IsNullOrEmpty(APIHub.JWT_TOKEN)) {
            background_select(false, true, false, true);
        } else {
            background_select(false, false, true, false);
        }
    }

    private void change_scene() {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(APIHub.JWT_TOKEN)) {
            Debug.Log("Enter key pressed");
            StartCoroutine(sceneLoader.play_animation());
        }
    }

    private void background_select(bool load, bool success, bool error, bool text) {
        loadingBG.SetActive(load);
        loginSuccessBG.SetActive(success);
        loginErrorBG.SetActive(error);
        textTMP.SetActive(text);
    }
}