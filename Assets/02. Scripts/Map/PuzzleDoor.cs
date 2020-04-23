using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{

    public bool[] puzzleTF;

    private Animation anim;
    private AudioSource audioSource;

    void Awake()
    {
        anim = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }

    void Switch(int value)
    {
        bool test = false;

        puzzleTF[value] = true;

        foreach(bool tf in puzzleTF)
        {
            if (tf)
                test = true;
            else
                test = false;
        }

        foreach(bool tf in puzzleTF)
        {
            if (!tf)
                test = false;
        }

        if (test)
        {
            anim.Play();
            audioSource.Play();
            Destroy(gameObject, 3);
        }
    }
}
