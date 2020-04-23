using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public string targetTag = "Enemy";
    public int damage = 5;

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
            col.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);

        if(col.gameObject.CompareTag("Player") && targetTag == "Player")
            col.gameObject.SendMessage("DestroyWeapon", damage, SendMessageOptions.DontRequireReceiver);

        if (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("Spear") || col.gameObject.CompareTag(targetTag))
        {
            rb2d.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }
}
