using System.Collections;
using System.Collections.Generic;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenInstruction : MonoBehaviour
{
    [SerializeField]
    private TMP_Text instructionTitle;

    [SerializeField]
    private float blinkSpeed = 2f;

    private float time;
    private Color textColor;
    private Player input;

    private void Start()
    {
        textColor = instructionTitle.color;
        input = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        time += Time.deltaTime * blinkSpeed;
        textColor.a = (Mathf.Sin(time) + 1) / 2f;
        instructionTitle.color = textColor;
        if (input.GetAnyButtonDown())
            GoToMenu();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("SCN_MainMenu");
    }
}
