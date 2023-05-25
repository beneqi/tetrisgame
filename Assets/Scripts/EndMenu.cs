using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
   
    
    public void QuitGame()
    { 
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Start Screen");
    }
    
}
