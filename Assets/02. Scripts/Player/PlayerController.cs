using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public static float hp;

    public float speed;
    public static bool flipY;
    public bool isGround;

    public static bool isDead;

    private float godTimer;

    public AudioSource jumpAudioSource;
    public AudioSource hitAudioSource;

    public AudioSource hpAudioSource;
    public static AudioSource healthAudioSource;

    public GameManager GM;

    public Image hpFill;
    public Image deadPanel;

    public GameObject playerDead;

    public GameObject jumpEffectPrefab;
    public GameObject hitEffectPrefab;

    private bool lKey;
    private bool rKey;

    private SpriteRenderer spriteRenderer;

    [HideInInspector]
    public Rigidbody2D rb2d;

    private Animation anim;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
    }

    void Start ()
    {
        flipY = false;
        isDead = false;

        healthAudioSource = hpAudioSource;

        hp = 100.0f;
    }

    void Update()
    {
        if(!isDead || CheatManager.God)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            if (Input.GetKeyDown(KeyCode.R))
                Dead();

            if(flipY)
                rb2d.gravityScale = -1;
            else
                rb2d.gravityScale = 1;

            if(Input.GetKeyUp(KeyCode.LeftArrow))
                lKey = false;

            if(Input.GetKeyUp(KeyCode.RightArrow))
                rKey = false;

            if(!rKey && !lKey)
            {
                if(rb2d.velocity.x > 0)
                    rb2d.velocity -= new Vector2(1,0) * 20 * Time.deltaTime;
                else if(rb2d.velocity.x < 0)
                    rb2d.velocity += new Vector2(1,0) * 20 * Time.deltaTime;
            }

            if(Input.GetKeyDown(KeyCode.C) && GameManager.coin >= 5 && hp < 100)
            {
                GameManager.coin -= 5;
                PlusHp();
            }

            if (godTimer > 0)
                godTimer -= Time.deltaTime;

            if (rb2d.velocity.y < -25)
                CameraFollow.Zoom(true, 8);
            else if(rb2d.velocity.y > 25)
                CameraFollow.Zoom(true, 10);
            else
                CameraFollow.Zoom(false, 16);
        }

        hpFill.fillAmount = hp / 100.0f;
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                LeftMove();

            if (Input.GetKey(KeyCode.RightArrow))
                RightMove();
        }
    }

    void LeftMove()
    {
        if(rb2d.velocity.x > -10)
            rb2d.AddForce(Vector2.left * speed * Time.fixedDeltaTime);

        lKey = true;
    }

    void RightMove()
    {
        if(rb2d.velocity.x < 10)
            rb2d.AddForce(Vector2.right * speed * Time.fixedDeltaTime);

        rKey = true;
    }
    
    void Jump()
    {
        if(isGround)
        {
            flipY = !flipY;

            Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);

            anim.Play("Player_Jump");
            AudioPlay(jumpAudioSource);

            isGround = false;
        }
    }

    void Hit(float value = 20)
    {
        if (hp >= value)
        {
            if(!CheatManager.God)
                hp -= value;

            if (hp < value)
                Dead();
            else
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

                StartCoroutine("HitFade");
                AudioPlay(hitAudioSource);

                CameraShake.Shake(0.3f, 0.125f);
            }
        }
    }

    void DestroyWeapon()
    {
        if(transform.childCount > 1)
            transform.GetChild(1).SendMessage("Release", SendMessageOptions.DontRequireReceiver);
    }

    void Dead()
    {
        // GetComponent<BoxCollider2D>().enabled = false;

        // rb2d.gravityScale = 1;
        // rb2d.AddForce(Vector2.up * speed / 10);

        isDead = true;

        hp = 0;
        
        GM.GameOver(1.5f);
        CameraShake.Shake(0.3f, 10);

        deadPanel.gameObject.SetActive(true);

        Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity);
        Instantiate(playerDead, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void AudioPlay(AudioSource audioSource, bool loop = false)
    {
        audioSource.loop = loop;
        audioSource.Play();
    }

    public static void PlusHp()
    {
        if(hp < 100)
            hp += 20;

        healthAudioSource.Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Floor"))
            isGround = true;

        if(col.gameObject.CompareTag("Spear") && godTimer <= 0)
        {
            // Dead();
            godTimer = 1;

            isGround = true;

            Hit();
            Jump();
        }

        rb2d.velocity = Vector2.zero;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Floor"))
            isGround = true;

        if (col.gameObject.CompareTag("Spear") && godTimer <= 0)
        {
            // Dead();
            godTimer = 1;

            isGround = true;

            Hit();
            Jump();
        }
        else if(col.gameObject.CompareTag("Spear") && godTimer <= 0.5f)
        {
            isGround =  true;
            Jump();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Floor"))
            isGround = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Portal"))
        {
            rb2d.simulated = false;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.position = col.gameObject.transform.position;
            anim.Play("Player_StageClear");

            if (transform.childCount > 1)
                transform.GetChild(1).SendMessage("Release", SendMessageOptions.DontRequireReceiver);

            GM.GameOver(3, true);
        }

        if (col.gameObject.CompareTag("CheckPoint"))
            col.gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
    }

    IEnumerator HitFade()
    {
        Color color = spriteRenderer.color;

        spriteRenderer.color = Color.cyan;

        color = Color.red;
        spriteRenderer.color = color;

        while (color.g < 1.0f && color.b < 1.0f && color.a > 0)
        {
            color.r -= 0.2f;
            color.g += 0.2f;
            color.b += 0.2f;

            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.02f);
        }
    }
}
