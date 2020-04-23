using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{

    public int menuNumber;

    public string[] menuStrs;
    public Text[] menuTexts;

    void OnEnable()
    {
        menuNumber = 0;

        Time.timeScale = 0;

        MenuChange();
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                menuNumber--;

                if(menuNumber < 0)
                    menuNumber = menuTexts.Length-1;

                MenuChange();
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                menuNumber++;

                if(menuNumber > menuTexts.Length-1)
                    menuNumber = 0;

                MenuChange();
            }

            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
                Enter();
        }
    }

    void Enter()
    {
        if(menuNumber == 1)
            SceneManager.LoadScene(0);
        else if(menuNumber == 2)
            Application.Quit();

        gameObject.SetActive(false);
    }

    void MenuChange()
    {
        for(int i = 0; i < menuTexts.Length; i++)
        {
            if(i == menuNumber)
                menuTexts[i].text = "▶ " + menuStrs[i];
            else
                menuTexts[i].text = menuStrs[i];
        }
    }
}
