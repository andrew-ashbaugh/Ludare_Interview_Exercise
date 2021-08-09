using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIScript : MonoBehaviour { 

    // This script houses all the ui functions, that we can use on the button components
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload current scene
    }

    public void Play()
    {
        SceneManager.LoadScene("Level_1"); // used for the main menu
    }

    public void Quit()
    {
        Application.Quit(); // quit the game
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // load the main menu
    }
}
