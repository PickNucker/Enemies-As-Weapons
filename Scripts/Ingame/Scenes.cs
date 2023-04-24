using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadBossScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
