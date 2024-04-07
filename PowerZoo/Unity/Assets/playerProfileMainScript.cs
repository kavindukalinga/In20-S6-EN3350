using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerProfileMainScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button saveButton;
    private PlayerProfileScript playerProfileScript;

    void Start()
    {
        playerProfileScript = GameObject.Find("PlayerProfileScript").GetComponent<PlayerProfileScript>();
        playerProfileScript.GetPlayerProfile();
        saveButton.onClick.AddListener(() => playerProfileScript.UpdateProfile());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
