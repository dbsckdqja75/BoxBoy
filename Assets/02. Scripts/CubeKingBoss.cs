using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKingBoss : MonoBehaviour
{

    public static bool isRaiding;
    public static bool switchPhase;

    public int phase;

    public bool[] puzzleTF;

    public GameObject player;

    public CompositeCollider2D col2d;

    public Animation anim;

    public AudioSource musicSource;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        phase = 0;
        isRaiding = false;
    }

    void Update()
    {
        if (isRaiding && musicSource.volume > 0)
            musicSource.volume -= Time.deltaTime;

        if ((!player) || (!isRaiding && switchPhase) && audioSource.volume > 0)
            audioSource.volume -= Time.deltaTime;
        else if (isRaiding)
            audioSource.volume = 0.3f;
    }

    public void NextPhase()
    {
        phase++;

        if (phase == 1)
        {
            anim.Play("1_ONE");
            audioSource.Play();
        }
        else if(phase == 2)
            anim.Play("2_TWO");
        else if (phase == 3)
            anim.Play("3_THREE");
        else if (phase == 4)
            anim.Play("5_END");
    }

    public void SwitchPhase()
    {
        if (!switchPhase)
            anim.Play("4_0_FAIL");
        else
            anim.Play("4_1_FOUR");
    }

    void Switch(int value)
    {
        bool test = false;

        puzzleTF[value] = true;

        foreach (bool tf in puzzleTF)
        {
            if (tf)
                test = true;
            else
                test = false;
        }

        foreach (bool tf in puzzleTF)
        {
            if (!tf)
                test = false;
        }

        if (test)
        {
            switchPhase = true;
        }
    }

    public void GameStart()
    {
        isRaiding = true;
        anim.Play("0_BEGIN");

        GameManager.timer = 400;

        Invoke("Col", 0.25f);
    }

    public void GameEnd()
    {
        isRaiding = false;
    }

    void Col()
    {
        col2d.isTrigger = false;
    }
}
