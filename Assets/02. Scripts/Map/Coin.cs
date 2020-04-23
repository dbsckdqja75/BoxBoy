using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.PlusCoin();
            StartCoroutine("ScaleDown");
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator ScaleDown()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(gameObject);
    }
}
