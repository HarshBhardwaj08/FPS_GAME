using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceeneMangement : MonoBehaviour
{
   
    public void ReloadGame()
    {
        SceneManager.LoadScene("Level");
        Debug.Log("Restart");
    }

    public void QuitGame()
    {
        Application.Quit(); 
        Debug.Log("Quit");
    }

}
