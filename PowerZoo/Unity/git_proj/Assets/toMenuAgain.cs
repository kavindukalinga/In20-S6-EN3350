using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toMenuAgain : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void aboutUs()
    {
        SceneManager.LoadScene("AboutUs");
    }

}
