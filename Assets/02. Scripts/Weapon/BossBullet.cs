using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    public string targetTag = "Player";
    public int damage = 20;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 6);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(targetTag))
        {
            col.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player") && targetTag == "Player")
            col.gameObject.SendMessage("DestroyWeapon", damage, SendMessageOptions.DontRequireReceiver);
    }
}
