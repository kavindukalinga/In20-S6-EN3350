using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    #region Variables

    public GameObject popup;

    private Camera _mainCamera;

    #endregion

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        
        string collName = rayHit.collider.gameObject.name;

        if ( collName == "animalShop"){
            Debug.Log("Animal shop detected");
            SceneManager.LoadScene("animalShopScene");
        }
        else if ((collName == "store1") || (collName == "store2") || (collName == "store3")){
            Debug.Log("Store is Detected");

            if (collName == "store1"){
                activeStallName.activeStall = 1;
            }
            else if (collName == "store2"){
                activeStallName.activeStall = 2;
            }
            else if (collName == "store3"){
                activeStallName.activeStall = 3;
            }

            Debug.Log("activeStall is now: " + activeStallName.activeStall);
            SceneManager.LoadScene("foodStallScene");
        }
        else if ((collName == "Lion") || (collName == "Tiger") || (collName == "Giraffe") || (collName == "Elephant") || (collName == "Zebra")){
            Debug.Log("Animal is Detected");

            if (collName == "Lion"){
                staticDataStore.staticData = "lion";
            }
            else if (collName == "Tiger"){
                staticDataStore.staticData = "tiger";
            }
            else if (collName == "Giraffe"){
                staticDataStore.staticData = "giraffe";
            }
            else if (collName == "Elephant"){
                staticDataStore.staticData = "elephant";
            }
            else if (collName == "Zebra"){
                staticDataStore.staticData = "zebra";
            }
            Debug.Log("Animal is now: " + staticDataStore.staticData);
            SceneManager.LoadScene("Shelter");
        }
        else if ( collName == "foodShop"){
            Debug.Log("Food shop detected");
            SceneManager.LoadScene("foodShopScene");
        }
        else if ( collName == "helpAvailable"){
            Debug.Log("Popup is shown");
            popup.SetActive(true);
        }
        else{
            Debug.Log(collName);
        }

        
    }

 
}
