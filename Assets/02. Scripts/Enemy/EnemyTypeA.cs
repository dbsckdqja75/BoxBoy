using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeA : MonoBehaviour
{
    public bool isDead;
    public bool isLR; // L - false | R - true

    public float speed = 300;
    public float rayDistance = 0.5f;

    public GameObject hitEffectPrefab;

    public SpriteRenderer[] spriteRenderer;

    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        int layerId = 0;
        int layerMask = 1 << layerId;

        RaycastHit2D hit;
        RaycastHit2D playerHit;

        if (!isDead)
        {
            if (isLR)
            {
                if (rb2d.velocity.x < 10)
                    rb2d.AddForce(Vector2.right * speed);

                hit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, layerMask);
                Debug.DrawRay(transform.position, transform.right * rayDistance, Color.red);

                playerHit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, 1 << 10);
            }
            else
            {
                if (rb2d.velocity.x > -10)
                    rb2d.AddForce(Vector2.left * speed);
                hit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, layerMask);
                Debug.DrawRay(transform.position, -transform.right * rayDistance, Color.red);

                playerHit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, 1 << 10);
            }

            if (hit.collider || playerHit.collider)
            {
                isLR = !isLR;

                if(playerHit.collider)
                {
                    if (playerHit.collider.gameObject.CompareTag("Player"))
                        playerHit.collider.gameObject.SendMessage("Hit", 20, SendMessageOptions.DontRequireReceiver);
                }

                rb2d.velocity = Vector2.zero;
            }
        }
    }

    void Hit()
    {
        isDead = true;

        GetComponent<Collider2D>().enabled = false;

        rb2d.velocity = Vector2.zero;
        rb2d.mass = 1;

        if(rb2d.gravityScale > 0)
            rb2d.AddForce(Vector2.up * 500);
        else
            rb2d.AddForce(Vector2.down * 500);

        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        StartCoroutine("HitFade");
        audioSource.Play();

        GameManager.PlusScore(150);

        Destroy(gameObject, 3);
    }

    IEnumerator HitFade()
    {
        Color color = spriteRenderer[0].color;

        while (color.r > 0 || color.g > 0 || color.b > 0)
        {
            color.r -= 0.2f;
            color.g -= 0.2f;
            color.b -= 0.2f;

            spriteRenderer[0].color = color;

            if (spriteRenderer.Length > 1)
                spriteRenderer[1].color = color;

            yield return new WaitForSeconds(0.02f);
        }
    }
}
