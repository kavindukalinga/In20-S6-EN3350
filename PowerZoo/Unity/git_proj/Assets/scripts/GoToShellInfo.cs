using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;  // Make sure to include this
using UnityEngine.UI;


public class GoToShellInfo : MonoBehaviour
{
    [SerializeField] Text shellName;  // Place this inside the class

    // Start is called before the first frame update
    public void GoToInfo()
    {
        string dataToKeep = shellName.text;
        staticDataStore.staticData = dataToKeep; 
        SceneManager.LoadScene("Shelter");
    }

}
