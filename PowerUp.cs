using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject pickUpEffect;
    public PlayerController player;
    public int powerUpIndex;
    public GameDataLog gameDataLog;
    // index: 
    // 1 is wall jump and stuff
    // 2 is double jump
    // 3 is dash

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        player  = FindObjectOfType<PlayerController>();
        gameDataLog = FindObjectOfType<GameDataLog>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        Instantiate(pickUpEffect, transform.position, transform.rotation);
        ThePowerUp();
        Destroy(gameObject);
    }

    private void ThePowerUp()
    {
        if (powerUpIndex == 1)
        {
            player.wallSlideAndJumpUnlocked = true;
            gameDataLog.Log_wallStuffUnlocked = true;
        }

        else if (powerUpIndex == 2)
        {
            player.doubleJumpUnlocked = true;
            player.maxNumberOfJumpsPermitted = 2;
            gameDataLog.Log_doubleJumpIsUnlocked = true;
        }

        else if (powerUpIndex == 3)
        {
            player.dashUnlocked = true;
            gameDataLog.log_dashIsUnlocked = true;

        }
    }
}
