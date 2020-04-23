using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingFloorCollider : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            col.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
