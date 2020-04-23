using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour
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
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(targetTag))
            col.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);

        if (col.gameObject.CompareTag("Player") && targetTag == "Player")
            col.gameObject.SendMessage("DestroyWeapon", damage, SendMessageOptions.DontRequireReceiver);

        if (col.gameObject.CompareTag("Floor"))
        {
            if(rb2d.gravityScale > 0)
                rb2d.AddForce(Vector2.up * 300);
            else
                rb2d.AddForce(Vector2.down * 300);
        }

        if (col.gameObject.CompareTag("Spear") || col.gameObject.CompareTag(targetTag))
        {
            rb2d.velocity = Vector2.zero;
            Destroy(gameObject);
        }

        if (rb2d.velocity.x == 0)
            Destroy(gameObject);
    }
}
