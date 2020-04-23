using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBounce : MonoBehaviour
{

    public bool isPlayer; // 플레이어의 소지 여부

    public GameObject bulletPrefab;
    public float fireInterval = 1;

    private float timer;

    private GameObject parent;

    private Rigidbody2D rb2d;
    private Animation anim;
    private AudioSource audioSource;
    private PlayerController player;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (parent)
            transform.localScale = new Vector3(0.5f / parent.transform.localScale.x, 0.5f / parent.transform.localScale.y, 0.5f / parent.transform.localScale.z);

        if (isPlayer && timer <= 0 && !GameManager.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.Z))
            {
                transform.localPosition = new Vector2(1, 0);

                Fire(Vector2.right);
            }

            if (Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.X))
            {
                transform.localPosition = new Vector2(-1, 0);

                Fire(Vector2.left);
            }
        }

        if (isPlayer)
            Flip();

        if (timer > 0)
            timer -= Time.deltaTime;
    }

    void Flip()
    {
        if (PlayerController.flipY && transform.localPosition.x > 0)
            transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
        else if (transform.localPosition.x > 0)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (PlayerController.flipY && transform.localPosition.x < 0)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        else if (transform.localPosition.x < 0)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    void Fire(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 500);

        if (player.rb2d.gravityScale < 0)
            bullet.GetComponent<Rigidbody2D>().gravityScale = -1;
        else
            bullet.GetComponent<Rigidbody2D>().gravityScale = 1;

        anim.Play("Weapon_Bounce_Fire");
        audioSource.Play();

        timer = fireInterval;
    }

    void Release()
    {
        parent = null;

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.transform.childCount > 1)
                col.gameObject.transform.GetChild(1).SendMessage("Release", SendMessageOptions.DontRequireReceiver);

            parent = col.gameObject;
            isPlayer = true;

            transform.SetParent(col.gameObject.transform);
            transform.localPosition = new Vector2(1, 0);

            player = col.gameObject.GetComponent<PlayerController>();
        }
    }
}
