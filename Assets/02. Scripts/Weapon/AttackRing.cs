using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRing : MonoBehaviour
{
    public bool isPlayer;
    public SpinRotation spinRotation;

    void Release()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

        if(col.gameObject.CompareTag("Player") && !isPlayer)
        {
            if (col.gameObject.transform.GetChild(0).childCount >= 0)
                col.gameObject.transform.GetChild(0).BroadcastMessage("Release", SendMessageOptions.DontRequireReceiver);

            isPlayer = true;

            transform.parent.SetParent(col.gameObject.transform.GetChild(0));
            transform.parent.position = col.gameObject.transform.position;
            transform.localPosition = new Vector2(0, 1.25f);

            spinRotation.speed = 180;
        }
    }
}
