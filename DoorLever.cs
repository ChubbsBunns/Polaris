using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLever : MonoBehaviour
{
    public Door door;
    public PlayerController player;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == ("Player"))
        {
            player = collider.gameObject.GetComponent<PlayerController>();
            if (player.actionButton)
            {
                door.openMe();
            }
        }
    }
}
