using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public KeyCode GodMode = KeyCode.F1; // 무적
    public KeyCode StageSkip = KeyCode.F2; // 스테이지 전환
    public KeyCode GetPistol = KeyCode.F3; // 권총 획득 (Z/X)
    public KeyCode GetBounce = KeyCode.F4; // 바운스볼 획득 (C) [0/3]
    public KeyCode GetAttackRing = KeyCode.F5; // 철퇴 고리 획득 [0/1]
    public KeyCode CoinInf = KeyCode.F6; // 코인 무한
    public KeyCode TimerInf = KeyCode.F7; // 타이머 무한
    public KeyCode SpearDelete = KeyCode.F8; // 맵의 가시들 삭제
    public KeyCode EnemyDelete = KeyCode.F9; // 맵의 적들 삭제
    public KeyCode P2P = KeyCode.F10; // 맵의 포탈로 이동
    public KeyCode OverCountZero = KeyCode.F11; // 게임 오버 횟수 초기화
    public KeyCode MapCleaner = KeyCode.F12; // 맵의 모든 아이템 삭제

    public GameObject weapon_pistol;
    public GameObject weapon_bounce;
    public GameObject attack_ring;

    public static bool God;

    private GameObject player_obj;
    private PlayerController player;

    private GameManager GM;

    private GameObject spear_obj;

    void Awake()
    {
        player_obj = GameObject.FindGameObjectWithTag("Player");
        player = player_obj.GetComponent<PlayerController>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        God = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(GodMode))
        {
            God = !God;

            if (!spear_obj && GameObject.Find("Tilemap_TrapSpear"))
                spear_obj = GameObject.Find("Tilemap_TrapSpear");

            if (!God && spear_obj)
                spear_obj.tag = "Spear";
            else if (God && spear_obj)
                spear_obj.tag = "Floor";
        }

        if (Input.GetKeyDown(StageSkip))
        {
            GM.LoadScene();
        }

        if (Input.GetKeyDown(GetPistol))
        {
            Instantiate(weapon_pistol, player_obj.transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(GetBounce))
        {
            Instantiate(weapon_bounce, player_obj.transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(GetAttackRing))
        {
            Instantiate(attack_ring, player_obj.transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(CoinInf))
        {
            GameManager.myCoin = 999;
            GameManager.coin = 999;
        }

        if (Input.GetKeyDown(TimerInf))
        {
            GameManager.timer = 999999;
        }

        if (Input.GetKeyDown(SpearDelete))
        {
            GameObject spear = null;

            if (GameObject.Find("Tilemap_TrapSpear"))
                spear = GameObject.Find("Tilemap_TrapSpear");

            if (spear)
                Destroy(spear);
        }

        if (Input.GetKeyDown(EnemyDelete))
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(GameObject enemy in enemys)
            {
                Destroy(enemy);
            }
        }

        if (Input.GetKeyDown(P2P))
        {
            player_obj.transform.position = GameObject.FindGameObjectWithTag("Portal").transform.position;
        }

        if (Input.GetKeyDown(OverCountZero))
        {
            GameManager.overCount = 0;
        }

        if(Input.GetKeyDown(MapCleaner))
        {
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
            GameObject[] healths = GameObject.FindGameObjectsWithTag("Health");
            GameObject[] timers = GameObject.FindGameObjectsWithTag("Timer");

            foreach (GameObject coin in coins)
            {
                Destroy(coin);
            }

            foreach (GameObject health in healths)
            {
                Destroy(health);
            }

            foreach (GameObject timer in timers)
            {
                Destroy(timer);
            }
        }
    }
}
