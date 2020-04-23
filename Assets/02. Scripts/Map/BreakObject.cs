using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{

    public float x;
    public float y;
    public float destroyTime = 3;

    private Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(x,y), ForceMode2D.Impulse);

        Destroy(gameObject, destroyTime);
	}
}
