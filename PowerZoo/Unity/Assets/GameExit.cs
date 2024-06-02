using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{

    public GameObject popupQuitGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitGamePopup(){
        Debug.Log("Not Quitting");
        popupQuitGame.SetActive(true);
        // Application.Quit();
    }

    public void quitGame(){
        Debug.Log("Game is quitting");
        Application.Quit();
    }
}
