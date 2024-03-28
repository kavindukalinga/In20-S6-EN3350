using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using TMPro;

public class PlayerProfileScript : MonoBehaviour
{
    public APIHubScript APIHub;
    private PlayerData playerData;
    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public TMP_InputField userName;
    public TMP_InputField nic;
    public TMP_InputField phoneNumber;
    public TMP_InputField email;
    public TMP_InputField profilePictureURL;
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";
    private string UpdateProfile_API = "http://20.15.114.131:8080/api/user/profile/update";

    public void GetPlayerProfile() => StartCoroutine(get_player_profile());
    public void UpdateProfile() {
        update_profile();
        StartCoroutine(put_request(UpdateProfile_API, playerData));
    } 

    private IEnumerator get_player_profile()
    {
        yield return StartCoroutine(APIHub.get_request(ViewProfile_API));

        if (APIHub.Response != null)
        {
            playerData = JsonUtility.FromJson<PlayerData>(APIHub.Response);
            Debug.Log(playerData);
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
        // firstName = GameObject.Find("FirstName").GetComponent<TMP_InputField>();
        // lastName = GameObject.Find("LastName").GetComponent<TMP_InputField>();
        // userName = GameObject.Find("UserName").GetComponent<TMP_InputField>();
        // nic = GameObject.Find("NIC").GetComponent<TMP_InputField>();
        // phoneNumber = GameObject.Find("PhoneNumber").GetComponent<TMP_InputField>();
        // email = GameObject.Find("Email").GetComponent<TMP_InputField>();
        // profilePictureURL = GameObject.Find("ProfilePictureURL").GetComponent<TMP_InputField>();

        Debug.Log("Player details: " + playerData.user.firstname + " " + playerData.user.lastname);
        firstName.text = playerData.user.firstname;
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

    private void update_profile()
    {
        playerData.user.firstname = firstName.text;
        playerData.user.lastname = lastName.text;
        playerData.user.username = null;
        playerData.user.nic = nic.text;
        playerData.user.phoneNumber = phoneNumber.text;
        playerData.user.email = email.text;
        playerData.user.profilePictureUrl = profilePictureURL.text;
    }

    private IEnumerator put_request(string url, PlayerData playerData) {
        if (string.IsNullOrEmpty(APIHub.JWT_TOKEN))
        {
            yield return StartCoroutine(APIHub.player_authenticate());
        }
        APIHub.Response = "";
        using (UnityWebRequest request = new UnityWebRequest(url, "PUT")) {
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(playerData.user)));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + APIHub.JWT_TOKEN);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                firstName.text = request.error;
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                APIHub.Response = jsonResponse;
                Debug.Log("Response: " + APIHub.Response);
            }
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public UserDataFromServer user;
    }

    [System.Serializable]
    public class UserDataFromServer
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
