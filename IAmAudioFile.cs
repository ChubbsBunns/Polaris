using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmAudioFile : MonoBehaviour
{

    private GameDataLog gameDataLog;
    // Start is called before the first frame update
    public int gdlog_wallstuff;
    public int gdlog_doublejump;
    public int gdlog_dash;

    [Header("My settings")]
    public bool playCriteria_wallStuff;
    public int wallStuff;
    public bool playCriteria_doubleJump;
    public int doubleJump;
    public bool playCriteria_dash;
    public int dash;

    public bool doIStillPlay;
    private void Awake()
    {
       gameDataLog = FindObjectOfType<GameDataLog>();
        if (gameDataLog.Log_wallStuffUnlocked)
        {
            gdlog_wallstuff = 1;
        }
        else if (gameDataLog.Log_wallStuffUnlocked == false)
        {
            gdlog_wallstuff = 0;
        }

        if (gameDataLog.Log_doubleJumpIsUnlocked)
        {
            gdlog_doublejump = 1;
        }

        else if (gameDataLog.Log_doubleJumpIsUnlocked)
        {
            gdlog_doublejump = 0;
        }

        if (gameDataLog.log_dashIsUnlocked)
        {
            gdlog_dash = 1;
        }

        else if (gameDataLog.log_dashIsUnlocked == false)
        {
            gdlog_dash = 0;
        }
    }
    void Start()
    {
        if (playCriteria_wallStuff)
        {
            wallStuff = 1;
        }

        else if (playCriteria_wallStuff == false)
        {
            wallStuff = 0;
        }

        if (playCriteria_doubleJump)
        {
            doubleJump = 1;
        }

        else if (playCriteria_doubleJump == false)
        {
            doubleJump = 0;
        }

        if (playCriteria_dash)
        {
            dash = 1;
        }

        else if (playCriteria_dash == false)
        {
            dash = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("check 1");
        {
            Debug.Log("check 2");
            if (gdlog_wallstuff == wallStuff && gdlog_doublejump == doubleJump && gdlog_dash == dash && doIStillPlay)
            {
                Debug.Log("MUSICCC");
                doIStillPlay = false;
            }
        }
    }

}
