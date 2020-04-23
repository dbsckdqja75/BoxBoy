using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{

    private Collider2D col2d;

    void Awake()
    {
        col2d = transform.parent.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col2d.enabled)
        {
            transform.parent.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
