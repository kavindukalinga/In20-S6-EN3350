using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class PlayerProfileScript : MonoBehaviour
{
    public APIHubScript APIHub;
    private PlayerData playerData;
    public InputField outputArea;
    public InputField lastName;
    public InputField userName;
    public InputField nic;
    public InputField phoneNumber;
    public InputField email;
    public InputField profilePictureURL;
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";

    public void GetPlayerProfile() => StartCoroutine(call_api()); 

    private IEnumerator call_api()
    {
        yield return StartCoroutine(APIHub.get_request(ViewProfile_API));

        if (APIHub.Response != null)
        {
            playerData = JsonUtility.FromJson<PlayerData>(APIHub.Response);
            Debug.Log("Player details: " + playerData.user.firstname + " " + playerData.user.lastname);
            load_data_to_UI();
        }
        else
        {
            Debug.LogError("Token response is null.");
        }
    }

    private void load_data_to_UI()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        lastName = GameObject.Find("LastName").GetComponent<InputField>();
        userName = GameObject.Find("UserName").GetComponent<InputField>();
        nic = GameObject.Find("NIC").GetComponent<InputField>();
        phoneNumber = GameObject.Find("PhoneNumber").GetComponent<InputField>();
        email = GameObject.Find("Email").GetComponent<InputField>();
        profilePictureURL = GameObject.Find("ProfilePictureURL").GetComponent<InputField>();

        outputArea.text = playerData.user.firstname;
        lastName.text = playerData.user.lastname;
        userName.text = playerData.user.username;
        nic.text = playerData.user.nic;
        phoneNumber.text = playerData.user.phoneNumber;
        email.text = playerData.user.email;
        // if (playerData.user.ProfilePictureUrl != null) {
        //     profilePictureUrl.text = playerData.user.ProfilePictureUrl;
        // }
        profilePictureURL.text = playerData.user.profilePictureUrl;
    }

    [System.Serializable]
    public class PlayerData
    {
        public User user;
    }

    [System.Serializable]
    public class User
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
        public string profilePictureUrl;
    }

}
