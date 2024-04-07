using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startQuizScript : MonoBehaviour
{

    public APIHubScript APIHub;
    public Button startQuizButton;

    void Start()
    {
        startQuizButton.onClick.AddListener(APIHub.RedirectQuiz);
    }

    
    
}


