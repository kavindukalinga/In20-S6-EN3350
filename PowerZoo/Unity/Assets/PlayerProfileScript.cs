using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerProfileScript : MonoBehaviour
{
    public APIHubScript APIHub;
    public sceneLoaderScript SceneLoader;
    public PlayerData playerData;
    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public TMP_InputField userName;
    public TMP_InputField nic;
    public TMP_InputField phoneNumber;
    public TMP_InputField email;
    public TMP_Text playerWarning;
    // public TMP_InputField profilePictureURL;
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";
    private string UpdateProfile_API = "http://20.15.114.131:8080/api/user/profile/update";

    public void GetPlayerProfile() => StartCoroutine(get_player_profile());
    public void UpdateProfile() {
        if (are_fields_empty()) {
            return;
        } else {
            update_profile();
            StartCoroutine(APIHub.put_request(UpdateProfile_API, playerData));
            StartCoroutine(SceneLoader.play_animation());
        }
    }

    private IEnumerator get_player_profile()
    {
        yield return StartCoroutine(APIHub.get_request(ViewProfile_API));

        if (APIHub.Response != null)
        {
            playerData = JsonUtility.FromJson<PlayerData>(APIHub.Response);
            Debug.Log(playerData);
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
        // profilePictureURL.text = playerData.user.profilePictureUrl;
    }

    private void update_profile()
    {
        playerData.user.firstname = firstName.text;
        playerData.user.lastname = lastName.text;
        playerData.user.username = null;
        playerData.user.nic = nic.text;
        playerData.user.phoneNumber = phoneNumber.text;
        playerData.user.email = email.text;
        // playerData.user.profilePictureUrl = profilePictureURL.text;
    }

    private bool are_fields_empty()
    {
        bool are_empty = false;
        TMP_InputField[] fields_array = {firstName, lastName, nic, phoneNumber, email};
        foreach (var field in fields_array)
        {
            if (string.IsNullOrEmpty(field.text))
            {
                playerWarning.text = "Please fill all the fields";
                are_empty = true;
                break;
            }
        }
        return are_empty;
        
    }

    // private void go_to_next_scene()
    // {
    //     if (is_quiz_completed())
    //     {
    //         Debug.Log("Quiz already completed");
    //         SceneManager.LoadScene("MenuScene");
    //     }
    //     else
    //     {
    //         Debug.Log("Starting quiz");
    //         StartCoroutine(APIHub.redirectQuiz());
    //     }
    // }



    // [System.Serializable]
    // public class QuizResponse
    // {
    //     public bool quizCompleted;
    // }

}


