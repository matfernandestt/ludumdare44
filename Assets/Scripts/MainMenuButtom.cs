using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtom : MonoBehaviour
{
    public void OnResumeGameButton()
    {
        SceneManager.LoadScene("SCN_LobbyStage");
    }

    public void OnNewGameButton()
    {
        SceneManager.LoadScene("SCN_LobbyStage");
    }

    public void OnCreditsButton()
    {

    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
