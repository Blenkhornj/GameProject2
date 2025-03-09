using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{


    public void StartGameButton() //button for loading main game scene
    {
        SceneManager.LoadScene("Main");
        
    }

    public void ExitGameButton() //button for game exit
    {
        Application.Quit();
        Debug.Log("Game exit"); //tests function within editor
    }
    

}