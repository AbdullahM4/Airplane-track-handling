using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;

    [Header("Main Menu Buttons")]
    public Button scenario1Button;
    public Button scenario2Button;
    public Button optionButton;
    public Button quitButton;
    public List<Button> returnButtons;

    [Header("Airplane Controller")]
    public AirplaneController1 airplaneController;

    void Start()
    {
        EnableMainMenu();

        // Hook events
        scenario1Button.onClick.AddListener(StartScenario1);
        scenario2Button.onClick.AddListener(StartScenario2);
        optionButton.onClick.AddListener(EnableOption);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var returnButton in returnButtons)
        {
            returnButton.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartScenario1()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(1);
        HideAll();
        if (airplaneController != null)
        {
            airplaneController.SetEngineOnFire(false);
        }
    }

    public void StartScenario2()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(2);
        HideAll();
        if (airplaneController != null)
        {
            airplaneController.SetEngineOnFire(true);
        }
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
}
