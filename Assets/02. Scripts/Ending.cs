using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{

    private AudioSource audioSource;
    private Animation anim;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();    
    }

    void Update()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 8;
            audioSource.pitch = 2;
        }
        else
        {
            Time.timeScale = 1;
            audioSource.pitch = 1;
        }
    }

    public void GoMain()
    {
        Time.timeScale = 1;
        audioSource.pitch = 1;

        SceneManager.LoadScene("Main");
    }
}
