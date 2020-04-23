using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeBomb : MonoBehaviour
{

    public float bombTime;
    public bool tf;

    private float timer;

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            timer = bombTime;
            tf = !tf;
        }

        if (tf)
            Zoom(true, 15);
        else
            Zoom(false, 15);
    }

    void Zoom(bool TF, int speed, int maxSize = 10)
    {
        if (TF && Camera.main.orthographicSize < maxSize)
        {
            Camera.main.orthographicSize += speed * Time.deltaTime;

            if (Camera.main.orthographicSize > maxSize)
                Camera.main.orthographicSize = maxSize;
        }
        else if (!TF && Camera.main.orthographicSize > 7)
        {
            Camera.main.orthographicSize -= speed * Time.deltaTime;

            if (Camera.main.orthographicSize < 7)
                Camera.main.orthographicSize = 7;
        }
    }
}
