using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using System;

public class PauseMenu : MonoBehaviour
{
    public GameObject wristUI;

    public bool actriveWristUI=true;
    // Start is called before the first frame update
    void Start()
    {
        DisplayWristUI();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            DisplayWristUI();
        }
    }
    public void DisplayWristUI()
    {
        if(actriveWristUI)
        {
            wristUI.SetActive(false);
            actriveWristUI = false;
            Time.timeScale = 1;
        }
        else if(!actriveWristUI)
        {
            wristUI.SetActive(true);
            actriveWristUI=true;
            Time.timeScale = 0;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void ExitGame()
{
    SceneManager.LoadScene(0);
}
}
