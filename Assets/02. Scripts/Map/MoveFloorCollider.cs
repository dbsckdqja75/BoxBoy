using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorCollider : MonoBehaviour
{

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.transform.SetParent(transform.parent);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.transform.parent = null;
    }
}
