using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackShelter : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToBack()
    {
        SceneManager.LoadScene("Shelter");
    }
}
