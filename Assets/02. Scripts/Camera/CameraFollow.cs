using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float zoomSize;
    public static bool zoomActive;

    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;

    private Transform temp;

    void Start()
    {
        zoomActive = false;
    }

    void FixedUpdate ()
    {
        if(target)
        {
            desiredPosition = target.position + offset;
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }

        if(zoomActive)
        {
            if (Camera.main.orthographicSize < zoomSize)
            {
                Camera.main.orthographicSize += 8 * Time.deltaTime;

                if (Camera.main.orthographicSize > zoomSize)
                    Camera.main.orthographicSize = zoomSize;
            }
        }

        if(!PlayerController.isDead)
            transform.position = smoothedPosition;
	}

    public void CameraPoint(Transform point, float size, bool active)
    {
        zoomActive = active;

        if (active)
        {
            temp = target;
            target = point;

            zoomSize = size;
        }
        else
            target = temp;
    }

    public static void Zoom(bool TF, int speed, int maxSize = 30)
    {
        if(!zoomActive)
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
}
