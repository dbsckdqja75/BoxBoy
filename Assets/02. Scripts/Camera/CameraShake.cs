using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform camTransform;

    public static float shakeAmount = 0.3f;
    public static float timer;

    private Vector3 originalPos;

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (timer > 0)
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        else
            camTransform.localPosition = originalPos;

        if (timer > 0)
            timer -= Time.deltaTime;
    }

    public static void Shake(float shakeTime, float amount)
    {
        timer = shakeTime;
        shakeAmount = amount;
    }
}
