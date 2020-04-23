using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRotation : MonoBehaviour
{
    public bool isParent;
    public float speed = 180;

    void Update()
    {
        if(transform.parent && isParent)
            transform.localScale = new Vector3(1 / transform.parent.transform.localScale.x, 1 / transform.parent.transform.localScale.y, 1 / transform.parent.transform.localScale.z);

        transform.RotateAround(transform.position, Vector3.back, speed * Time.deltaTime);
    }
}
