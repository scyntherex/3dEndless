using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    GameObject[] panels;
    GameObject[] mainMenuBtns;
    int maxLives = 3;

    void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("subpanel");
        mainMenuBtns = GameObject.FindGameObjectsWithTag("MainMenuBtn");

        foreach(GameObject obj in panels)
        {
            obj.SetActive(false);
        }
    }

    public void ClosePanel(Button button)
    {
        button.gameObject.transform.parent.gameObject.SetActive(false);
        foreach (GameObject obj in mainMenuBtns)
        {
            obj.SetActive(true);
        }
    }
    public void OpenPanel(Button button)
    {
        button.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        foreach (GameObject obj in mainMenuBtns)
        {
            if (obj != button.gameObject)
            {
                obj.SetActive(false);
            }
        }
    }

    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("lives", maxLives);
        SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }
    }
}
