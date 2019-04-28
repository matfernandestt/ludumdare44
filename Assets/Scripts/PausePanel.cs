using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panelHolder;

    public static PausePanel Instance;

    private Player input;
    private bool isEnabled = false;

    private void Awake()
    {
        Instance = this;
        input = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (!input.GetButtonDown("Pause")) return;

        if(isEnabled)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isEnabled = true;
        Time.timeScale = 0f;
        panelHolder.SetActive(true);
    }

    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isEnabled = false;
        Time.timeScale = 1f;
        panelHolder.SetActive(false);
    }

    public void OnResumeButton()
    {
        Hide();
    }

    public void OnReturnToLobbyButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SCN_LobbyStage");
    }

    public void OnReturnToMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SCN_MainMenu");
    }

}
