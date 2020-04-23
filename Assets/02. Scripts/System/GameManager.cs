using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static bool isGameOver;
    public static int overCount;
    public static int score;
    public static int coin;

    public static int myCoin;

    public string nextScene;

    public Image gameOverPanel;
    public Text over_stageText;
    public Text retryText;

    public Image gameInfoPanel;
    public Text scoreText;
    public Text coinText;
    public Text info_stageText;
    public Text timerText;

    public AudioSource coinSound;
    public static AudioSource coinAudioSource;

    public static float timer;

    private AudioSource audioSource;
    private GameObject gamePause;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        coinAudioSource = coinSound;
        gamePause = GameObject.Find("UI").transform.Find("GamePause").gameObject;
    }

    void Start()
    {
        Gravity(0, -40);

        score = 0;
        timer = 400;
        isGameOver = false;

        coin = myCoin;

        info_stageText.text = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (timer > 0)
        {
            // timer -= Time.deltaTime;
            timerText.text = "TIME : " + timer.ToString("0");

            gameInfoPanel.fillAmount = timer / 400;
        }
        else if(!isGameOver)
            GameOver(0);

        scoreText.text = "SCORE : " + score.ToString("000000");
        coinText.text = "x " + coin.ToString();

        if(Input.GetKeyDown(KeyCode.Escape))
            gamePause.SetActive(!gamePause.activeSelf);

        if (isGameOver && audioSource.volume > 0)
            audioSource.volume -= Time.deltaTime;
    }

    public void GameOver(float time = 3, bool isClear = false)
    {
        CleanUI();

        if(isClear)
            Invoke("GameClear", time);
        else
            Invoke("OnGameOverUI", time);
    }

    void GameClear()
    {
        gameOverPanel.gameObject.SetActive(true);

        overCount = 0;

        over_stageText.text = nextScene.ToString();
        retryText.text = "■ x " + overCount.ToString();

        myCoin = coin;

        if (myCoin > 999)
            myCoin = 999;

        Invoke("LoadScene", 3);
    }

    void OnGameOverUI()
    {
        if (isGameOver)
        {
            gameOverPanel.gameObject.SetActive(true);
            
            overCount++;

            nextScene = SceneManager.GetActiveScene().name;
            over_stageText.text = SceneManager.GetActiveScene().name;
            retryText.text = "■ x " + overCount.ToString();

            Invoke("LoadScene", 1.5f);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void CleanUI()
    {
        isGameOver = true;

        gameInfoPanel.GetComponent<Animation>().Play("GameInfo_Close");
    }

    public static void Gravity(float x = 0, float y = -40.0f)
    {
        Physics2D.gravity = new Vector2(x, y);
    }

    public static void PlusTime(float value)
    {
        if(timer < 999)
            timer += value;

        if (timer > 400)
            timer = 400;
    }

    public static void PlusScore(int value)
    {
        score += value;
    }

    public static void PlusCoin()
    {
        if(coin < 999)
            coin++;

        coinAudioSource.Play();
    }
}
