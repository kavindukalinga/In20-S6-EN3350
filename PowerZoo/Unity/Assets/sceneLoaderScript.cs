using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneLoaderScript : MonoBehaviour
{
    public APIHubScript APIHub;
    public loginScript LoginScript;
    public Animator transition;
    public float transitionTime = 1f;
    private bool menuLoaded = false;

    // Update is called once per frame
    void Update()
    {
        change_scene();
    }

    private void change_scene() {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(APIHub.JWT_TOKEN) && !menuLoaded) {
            Debug.Log("Enter key pressed");
            menuLoaded = true;
            // LoginScript.textTMP.SetActive(false);
            StartCoroutine(play_animation());
        }
    }

    private IEnumerator play_animation() {
        // Play the animation
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        yield return StartCoroutine(APIHub.check_quiz_completed());
        // Load the next scene
        Debug.Log(APIHub.quizResponse.quizCompleted);
        load_next_scene(APIHub.quizResponse.quizCompleted);
    }

    private void load_next_scene(bool quizCompleted) {
        if (quizCompleted) {
            SceneManager.LoadScene("MenuScene");
        }
        else {
            SceneManager.LoadScene("PlayerProfileScene");
        }
    }
}
