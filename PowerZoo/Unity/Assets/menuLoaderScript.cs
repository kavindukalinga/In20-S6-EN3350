using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuLoaderScript : MonoBehaviour
{

    public loginScript LoginScript;
    public Animator transition;
    public float transitionTime = 1f;
    private bool menuLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        LoginScript = GameObject.Find("LoginScript").GetComponent<loginScript>();
    }

    // Update is called once per frame
    void Update()
    {
        change_scene();
    }

    private void change_scene() {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(LoginScript.JWT_TOKEN) && !menuLoaded) {
            menuLoaded = true;
            LoginScript.textTMP.SetActive(false);
            StartCoroutine(play_animation());
        }
    }

    private IEnumerator play_animation() {
        // Play the animation
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        // Load the next scene
        SceneManager.LoadScene("MenuScene");
    }
}
