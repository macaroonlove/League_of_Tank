using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_UI : MonoBehaviour
{
    public GameObject Menu_game_UI;
    public void OnContinue()
    {
        Time.timeScale = 1;
        Menu_game_UI.SetActive(false);
    }

    public void OnRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OnOutMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
