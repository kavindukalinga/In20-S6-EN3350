using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image border1;
    public Image border2;
    public Image border4;
    public Image border5;
    public Image border6;
    // public TMP_InputField profilePictureURL;
    private string ViewProfile_API = "http://20.15.114.131:8080/api/user/profile/view";
    private string UpdateProfile_API = "http://20.15.114.131:8080/api/user/profile/update";

    public void GetPlayerProfile() => StartCoroutine(get_player_profile());
    public void UpdateProfile() {
        if (are_fields_invalid()) {
            return;
        } else {
            update_profile();
            StartCoroutine(APIHub.put_request(UpdateProfile_API, playerData));
            StartCoroutine(SceneLoader.play_animation());
        }
    }

    private IEnumerator get_player_profile() 
    {
        Debug.Log("Getting player profile");
        yield return StartCoroutine(APIHub.get_request(ViewProfile_API));
        Debug.Log(APIHub.Response);

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

    private bool are_fields_invalid()
    {
        bool are_empty = false;

        if (string.IsNullOrEmpty(playerData.user.firstname)) {
            playerWarning.text = "*Required feild or feilds are empty";
            border1.color = Color.red;
            are_empty = true;
        }
        if (string.IsNullOrEmpty(playerData.user.lastname))
        {
            playerWarning.text = "*Required feild or feilds are empty";
            border2.color = Color.red;
            are_empty = true;
        }

        if (string.IsNullOrEmpty(playerData.user.nic))
        {
            playerWarning.text = "*Required feilds are empty";
            border5.color = Color.red;
            are_empty = true;
        }

        if (string.IsNullOrEmpty(playerData.user.phoneNumber))
        {
            playerWarning.text = "*Required feilds are empty";
            border6.color = Color.red;
            are_empty = true;
        }

        if (string.IsNullOrEmpty(playerData.user.email))
        {
            playerWarning.text = "*Required feilds are empty";
            border4.color = Color.red;
            are_empty = true;
        }
        if (are_empty == false && isvalidMobileNo() == false)
        {
            playerWarning.text = "*Invalid mobile number";
            border6.color = Color.red;
            are_empty = true;
        }
        else if (are_empty == false && isvalidNic() == false)
        {
            playerWarning.text = "*Invalid nic number";
            border5.color = Color.red;
            are_empty = true;
        }
        else if (are_empty == false && isvalidEmail() == false)
        {
            playerWarning.text = "*Invalid Email address";
            border4.color = Color.red;
            are_empty = true;
        }
        else if (are_empty == false) { playerWarning.text = ""; }
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

    private bool isvalidMobileNo()
    {
        if (phoneNumber.text.Length != 10 || phoneNumber.text[0] != '0')
        {
            return false;
        }
        string text = phoneNumber.text;
        bool allDigits = true;
        foreach (char c in text)
        {
            if (!char.IsDigit(c))
            {
                allDigits = false;
                break;
            }
        }
        if (allDigits == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool isvalidNic()
    { 
        if (nic.text.Length != 12 && nic.text.Length != 10)
        {
            return false;
        }
        string text;
        if (nic.text.Length == 12) { text = nic.text; }
        else { text = nic.text.Substring(0, 9); }
        bool allDigits = true;
        foreach (char c in text)
        {
            if (!char.IsDigit(c))
            {
                allDigits = false;
                break;
            }
        }
        if (allDigits == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool isvalidEmail() 
    {
        char characterToCheck = '@';
        bool isCharacter1Present = false;
        bool isCharacter2Present = false;
        int count = 0;
        string text = email.text;

        foreach (char c in text)
        {
            if (c == characterToCheck)
            {
                isCharacter1Present = true;
                break;
            }
        }

        characterToCheck = '.';
        foreach (char c in text)
        {
            if (c == characterToCheck)
            {
                isCharacter2Present = true;
                count++;
            }
        }

        if (isCharacter1Present && isCharacter2Present && count == 1) { return true; }
        else { return false; }
    }

}


