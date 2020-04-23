using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{

    public Vector2 destination;
    public float speed = 3;
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
        timer = waitTime;
    }

    void Update()
    {
        if(destination.x > 0)
        {
            if (transform.position.x >= point[1].x && timer <= 0)
            {
                number = 0;
                isArrive = true;

                timer = waitTime;
            }
            else if (transform.position.x <= point[0].x && timer <= 0)
            {
                number = 1;
                isArrive = true;

                timer = waitTime;
            }
        }
        else
        {
            if (transform.position.x <= point[1].x && timer <= 0)
            {
                number = 0;
                isArrive = true;

                timer = waitTime;
            }
            else if (transform.position.x >= point[0].x && timer <= 0)
            {
                number = 1;
                isArrive = true;

                timer = waitTime;
            }
        }

        if (timer > 0 && isArrive)
            timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (timer <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, point[number], speed * Time.fixedDeltaTime);
            isArrive = false;
        }
    }
}
