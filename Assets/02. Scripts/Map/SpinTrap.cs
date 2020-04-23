using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTrap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.SendMessage("Hit", 20, SendMessageOptions.DontRequireReceiver);
    }
}
