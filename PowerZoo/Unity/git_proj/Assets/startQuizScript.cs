using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class startQuizScript : MonoBehaviour
{

    // public APIHubScript APIHub;
    public Button startQuizButton;
    public Button continueQuizButton;
    public Button aboveButton;
    public Button belowButton;
    public Button leftButton;
    public Button rightButton;
    public GameObject popup;
    public TextMeshProUGUI startQuizButtonText;
    public bool quizStarted = false;

    void Start()
    {
        startQuizButton.onClick.AddListener(startQuiz);
        continueQuizButton.onClick.AddListener(continueQuiz);
        aboveButton.onClick.AddListener(hidePopup);
        belowButton.onClick.AddListener(hidePopup);
        leftButton.onClick.AddListener(hidePopup);
        rightButton.onClick.AddListener(hidePopup);
    }

    private void startQuiz()
    {
        if (!quizStarted)
        {
            APIHubScript.Instance.RedirectQuiz();
            startQuizButtonText.text = "Continue";
            quizStarted = true;
        } else {
            StartCoroutine(checkQuizCompleted());
        }
    }

    private void continueQuiz() {
        APIHubScript.Instance.RedirectQuiz();
        popup.SetActive(false);
    }

    private void sceneControler(bool isAnswered)
    {
        if (!isAnswered)
        {
            showPopup();
        } else {
            SceneManager.LoadScene("MenuScene");
        }
    }

    private IEnumerator checkQuizCompleted()
    {
        yield return StartCoroutine(APIHubScript.Instance.check_quiz_completed());
        sceneControler(APIHubScript.Instance.quizResponse.isAnswered);
    }
    
    private void showPopup()
    {
        popup.SetActive(true);
    }

    private void hidePopup()
    {
        popup.SetActive(false);
    }
}


