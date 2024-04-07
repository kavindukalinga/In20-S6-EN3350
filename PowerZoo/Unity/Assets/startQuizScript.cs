using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class startQuizScript : MonoBehaviour
{

    public APIHubScript APIHub;
    public Button startQuizButton;
    public Button continueQuizButton;
    public GameObject popup;
    public TextMeshProUGUI startQuizButtonText;
    public bool quizStarted = false;

    void Start()
    {
        startQuizButton.onClick.AddListener(startQuiz);
        continueQuizButton.onClick.AddListener(continueQuiz);
    }

    private void startQuiz()
    {
        if (!quizStarted)
        {
            APIHub.RedirectQuiz();
            startQuizButtonText.text = "Continue";
            quizStarted = true;
        } else {
            StartCoroutine(checkQuizCompleted());
        }
    }

    private void continueQuiz() {
        APIHub.RedirectQuiz();
        popup.SetActive(false);
    }

    private void sceneControler(bool quizCompleted)
    {
        if (!quizCompleted)
        {
            showPopup();
        } else {
            SceneManager.LoadScene("MenuScene");
        }
    }

    private IEnumerator checkQuizCompleted()
    {
        yield return StartCoroutine(APIHub.check_quiz_completed());
        sceneControler(APIHub.quizResponse.quizCompleted);
    }
    
    private void showPopup()
    {
        popup.SetActive(true);
    }
}


