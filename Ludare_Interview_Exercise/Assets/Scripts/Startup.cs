using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Startup : MonoBehaviour
{
    // We can have all sorts of stuff in our startup scene, but we want to quickly switch to the menu after the inital load
    void Start()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

}
