using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    public bool isStageMenu;

    public int stageNumber;
    public string[] stageNames;

    public Image stageLoadanel;
    public Text stageTitle;

    public string[] stageStrs;
    public Text[] stageTexts;

    public AudioSource musicSource;

    private bool isLoad;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isStageMenu)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                stageNumber--;

                if (stageNumber < 0)
                    stageNumber = stageTexts.Length - 1;

                MenuChange();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                stageNumber++;

                if (stageNumber > stageTexts.Length - 1)
                    stageNumber = 0;

                MenuChange();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
                Enter();

            if (Input.GetKeyDown(KeyCode.Escape))
                isStageMenu = false;
        }

        if(isLoad && musicSource.volume > 0)
            musicSource.volume -= 0.1f * Time.deltaTime;
    }

    public void OnActive()
    {
        isStageMenu = true;
        stageNumber = 0;

        MenuChange();
    }

    void Enter()
    {
        stageTitle.text = stageNames[stageNumber];
        stageLoadanel.gameObject.SetActive(true);

        audioSource.Play();

        isLoad = true;

        Invoke("LoadScene", 3);

        isStageMenu = false;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(stageNumber+1);
    }

    void MenuChange()
    {
        for (int i = 0; i < stageTexts.Length; i++)
        {
            if (i == stageNumber)
                stageTexts[i].text = "▶ " + stageStrs[i];
            else
                stageTexts[i].text = stageStrs[i];
        }

        audioSource.Play();
    }
}
