using UnityEngine;
using UnityEngine.SceneManagement;  // Make sure to include this
using UnityEngine.UI;


public class GoToFood : MonoBehaviour
{

    // Start is called before the first frame update
    public void GoToInfo()
    {
        SceneManager.LoadScene("food");
    }

}
