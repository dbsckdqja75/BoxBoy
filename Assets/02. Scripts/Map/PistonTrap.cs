using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonTrap : MonoBehaviour
{

    public Vector2 destination;

    public float speed = 3;
    public float backSpeed = 3;

    public float waitTime = 1.5f;

    private Vector2[] point = new Vector2[2]; // 0 - Start | 1 - End

    private bool isArrive;
    private float timer;
    private int number;

    void Start()
    {
        point[0] = transform.position;

        point[1] = point[0];
        point[1] += destination;

        isArrive = true;

        number = 1;
        timer = waitTime;
    }

    void Update()
    {
        if (destination.y > 0)
        {
            if (transform.position.y >= point[1].y && timer <= 0)
            {
                number = 0;
                isArrive = true;

                timer = waitTime;
            }
            else if (transform.position.y <= point[0].y && timer <= 0)
            {
                number = 1;
                isArrive = true;

                timer = waitTime;
            }
        }
        else
        {
            if (transform.position.y <= point[1].y && timer <= 0)
            {
                number = 0;
                isArrive = true;

                timer = waitTime;
            }
            else if (transform.position.y >= point[0].y && timer <= 0)
            {
                number = 1;
                isArrive = true;

                timer = waitTime;
            }
        }

        if (timer > 0 && isArrive)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (number == 1)
                transform.position = Vector2.MoveTowards(transform.position, point[number], speed * Time.deltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position, point[number], backSpeed * Time.deltaTime);

            isArrive = false;
        }
    }
}
