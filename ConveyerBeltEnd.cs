using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBeltEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Moving Platform"))
        {
            Destroy(collision.gameObject);
        }

        else
        {
            return;
        }
    }
}
