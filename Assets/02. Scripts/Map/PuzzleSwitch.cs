using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitch : MonoBehaviour
{

    public int value;
    public bool exit;

    public bool isToggle;

    private bool isActive;

    public GameObject obj;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if((obj && !isActive) || (obj && isToggle))
            {
                obj.SendMessage("Switch", value, SendMessageOptions.DontRequireReceiver);
                isActive = true;
            }

            if(GetComponent<SpriteRenderer>())
                GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && exit)
            obj.SendMessage("Switch", value+1, SendMessageOptions.DontRequireReceiver);
    }
}
