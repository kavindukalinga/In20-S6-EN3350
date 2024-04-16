using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerProfileMainScript : MonoBehaviour
{
    public Button saveButton;
    private PlayerProfileScript playerProfileScript;

    void Start()
    {
        playerProfileScript = GameObject.Find("PlayerProfileScript").GetComponent<PlayerProfileScript>();
        playerProfileScript.GetPlayerProfile();
        saveButton.onClick.AddListener(() => playerProfileScript.UpdateProfile());
    }
}
