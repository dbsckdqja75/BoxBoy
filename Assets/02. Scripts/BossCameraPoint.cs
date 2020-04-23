using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraPoint : MonoBehaviour
{

    public float size = 15;
    public CameraFollow CF;
    public CubeKingBoss cubeKingBoss;

    void Switch(int value)
    {
        if (value == 0)
        {
            CF.CameraPoint(transform, size, true);

            if(!CubeKingBoss.isRaiding)
                cubeKingBoss.GameStart();
        }
        else if (value == 1)
            CF.CameraPoint(transform, size, false);
    }
}
