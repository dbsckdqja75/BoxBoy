using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{

    public float size;
    public CameraFollow CF;

    void Switch(int value)
    {
        if (value == 0)
            CF.CameraPoint(transform, size, true);
        else if(value == 1)
            CF.CameraPoint(transform, size, false);
    }
}
