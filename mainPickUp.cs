using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainPickUp : MonoBehaviour
{
    public GameObject pickUpEffect;
    public int mainPickUpIndex;
    public GameDataLog gameDataLog;

    private void Start()
    {
        gameDataLog = FindObjectOfType<GameDataLog>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
        if (mainPickUpIndex == 1)
        {
            gameDataLog.propellant= true;
        }

        else if (mainPickUpIndex == 2)
        {
            gameDataLog.targetting_Module = true;
        }

        else if (mainPickUpIndex == 3)
        {
            gameDataLog.oxygen_Tanks = true;
        }
    }
}