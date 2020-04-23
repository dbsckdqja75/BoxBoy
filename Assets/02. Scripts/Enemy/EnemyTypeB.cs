using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeB : MonoBehaviour
{
    public bool isDead;
    public bool isLR; // L - false | R - true

    public float speed = 300;
    public float bulletSpeed = 500;
    public float rayDistance = 0.5f;
    public float bulletDistance = 5;

    public Transform[] firePoint; // L - 0 | R - 1
    public GameObject bulletPrefab;

    public float fireInterval = 1;
    private float timer;

    public GameObject hitEffectPrefab;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        int layerId = 0;
        int layerMask = 1 << layerId;

        RaycastHit2D hit;
        RaycastHit2D attackHit;

        RaycastHit2D playerHit;

        if (!isDead)
        {
            if (isLR)
            {
                if (rb2d.velocity.x < 10)
                    rb2d.AddForce(Vector2.right * speed);

                hit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, layerMask);
                Debug.DrawRay(transform.position, transform.right * rayDistance, Color.red);

                attackHit = Physics2D.Raycast(transform.position + Vector3.right * 0.6f, Vector3.right, bulletDistance);
                Debug.DrawRay(transform.position + Vector3.right * 0.6f, transform.right * bulletDistance, Color.green);

                playerHit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, 1 << 10);
            }
            else
            {
                if (rb2d.velocity.x > -10)
                    rb2d.AddForce(Vector2.left * speed);
                hit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, layerMask);
                Debug.DrawRay(transform.position, -transform.right * rayDistance, Color.red);

                attackHit = Physics2D.Raycast(transform.position + Vector3.left * 0.6f, Vector3.left, bulletDistance);
                Debug.DrawRay(transform.position + Vector3.left * 0.6f, -transform.right * bulletDistance, Color.green);

                playerHit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, 1 << 10);
            }

            if (hit.collider || playerHit.collider)
            {
                isLR = !isLR;

                if (playerHit.collider)
                {
                    if (playerHit.collider.gameObject.CompareTag("Player"))
                        playerHit.collider.gameObject.SendMessage("Hit", 20, SendMessageOptions.DontRequireReceiver);
                }

                rb2d.velocity = Vector2.zero;
            }

            if(attackHit.collider)
            {
                Debug.Log(attackHit.collider.gameObject);

                if(attackHit.collider.gameObject.CompareTag("Player"))
                {
                    if(timer <= 0)
                        Attack();
                }
            }

            if (timer > 0)
                timer -= Time.deltaTime;
        }
    }

    void Attack()
    {
        if (isLR)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint[1].position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletSpeed);
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint[0].position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletSpeed);
        }

        timer = fireInterval;
    }

    void Hit()
    {
        isDead = true;

        GetComponent<Collider2D>().enabled = false;

        rb2d.velocity = Vector2.zero;
        rb2d.mass = 1;

        if (rb2d.gravityScale > 0)
            rb2d.AddForce(Vector2.up * 500);
        else
            rb2d.AddForce(Vector2.down * 500);

        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        StartCoroutine("HitFade");
        audioSource.Play();

        GameManager.PlusScore(300);

        Destroy(gameObject, 3);
    }

    IEnumerator HitFade()
    {
        Color color = spriteRenderer.color;

        while (color.r > 0 || color.g > 0 || color.b > 0)
        {
            color.r -= 0.2f;
            color.g -= 0.2f;
            color.b -= 0.2f;

            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.02f);
        }
    }
}
