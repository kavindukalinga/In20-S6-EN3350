using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostMethod : MonoBehaviour
{
    public InputField outputArea;
    public APIHubScript APIHub;
    public PlayerProfileScript playerProfile;

    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        playerProfile.GetPlayerProfile();
        // GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(APIHub.Authenticate);
        // APIHub.ViewProfile();
    }


}

