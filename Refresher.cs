using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Refresher : MonoBehaviour
{
    public GameDataLog gameDataLog;
    public CameraFollowPlayer vcamRefresh;
    public PowerUp powerUp;
    // Start is called before the first frame update
    void Awake()
    {
        gameDataLog = FindObjectOfType<GameDataLog>();
        gameDataLog.RefreshRevivePoint();
        vcamRefresh = FindObjectOfType<CameraFollowPlayer>();
        vcamRefresh.FollowPlayer();
        powerUp = FindObjectOfType<PowerUp>();
        if (powerUp = null)
        {
            return;
        }

    }
}
