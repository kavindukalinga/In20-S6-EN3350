using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneLoaderScript : MonoBehaviour
{
    // public APIHubScript APIHub
    public Animator transition;
    public float transitionTime = 1f;
    public string sceneA;
    public string sceneB;
    

    public IEnumerator play_animation() {
        // Play the animation
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        yield return StartCoroutine(APIHubScript.Instance.check_quiz_completed());
        // Load the next scene
        Debug.Log(APIHubScript.Instance.quizResponse);
        load_next_scene(APIHubScript.Instance.quizResponse.isAnswered, sceneA, sceneB);
    }

    private void load_next_scene(bool isAnswered, string sceneA, string sceneB) {
        if (isAnswered) {
            SceneManager.LoadScene(sceneA);
        }
        else {
            SceneManager.LoadScene(sceneB);
        }
    }
}
