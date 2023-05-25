using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverEffect;
    public void QuitGame()
    { 
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Start Screen");
    }
}
