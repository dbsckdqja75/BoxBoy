using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public bool isMainMenu;

    public int menuNumber;

    public Image stageSelect;
    public Image guidePanel;

    public string[] menuStrs;
    public Text[] menuTexts;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameManager.coin = 0;
        GameManager.myCoin = 0;

        isMainMenu = true;
        menuNumber = 0;

        MenuChange();
    }

    void Update()
    {
        if(isMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                menuNumber--;

                if (menuNumber < 0)
                    menuNumber = menuTexts.Length - 1;

                MenuChange();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                menuNumber++;

                if (menuNumber > menuTexts.Length - 1)
                    menuNumber = 0;

                MenuChange();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
                Enter();
        }

        if(!isMainMenu)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (stageSelect.gameObject.activeSelf)
                    stageSelect.gameObject.SetActive(false);

                if (guidePanel.gameObject.activeSelf)
                    guidePanel.gameObject.SetActive(false);

                isMainMenu = true;
            }
        }
    }

    void Enter()
    {
        if (menuNumber == 0)
            stageSelect.gameObject.SetActive(true);
        else if (menuNumber == 1)
            guidePanel.gameObject.SetActive(true);
        else if (menuNumber == 2)
            Application.Quit();

        audioSource.Play();

        isMainMenu = false;
    }

    void MenuChange()
    {
        for (int i = 0; i < menuTexts.Length; i++)
        {
            if (i == menuNumber)
                menuTexts[i].text = "▶ " + menuStrs[i];
            else
                menuTexts[i].text = menuStrs[i];
        }

        audioSource.Play();
    }
}
