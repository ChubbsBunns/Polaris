using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalscript : MonoBehaviour
{
    GameDataLog gameDataLog;

    private void Start()
    {
        gameDataLog = FindObjectOfType<GameDataLog>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameDataLog.log_dashIsUnlocked && gameDataLog.Log_doubleJumpIsUnlocked && gameDataLog.Log_wallStuffUnlocked)
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }
}
