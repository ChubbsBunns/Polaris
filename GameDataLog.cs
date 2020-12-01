using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataLog : MonoBehaviour
{
    public int nextActivePortalNumber;
    public GameObject player;
    public RevivePoint currentRevivePointObject;
    public Transform currentRevivePoint;

    [Header("Player Unlocks")]
    public bool log_dashIsUnlocked;
    public bool Log_doubleJumpIsUnlocked;
    public bool Log_wallStuffUnlocked;

    public bool propellant;
    public bool targetting_Module;
    public bool oxygen_Tanks;

    [Header("Audio logs")]
    public bool mainMonoLogue;


    private void Awake()
    {
        currentRevivePointObject = FindObjectOfType<RevivePoint>();
        currentRevivePoint = currentRevivePointObject.transform;
    }
    private void Start()
    {
        if (FindObjectsOfType<GameDataLog>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        if (FindObjectsOfType<PlayerController>().Length < 1)
        {
            ReviveHere(currentRevivePoint);
        }
    }

    public void RefreshRevivePoint()
    {
        currentRevivePointObject = FindObjectOfType<RevivePoint>();
        currentRevivePoint = currentRevivePointObject.transform;
        if (FindObjectsOfType<PlayerController>().Length < 1)
        {
            ReviveHere(currentRevivePointObject.transform);
        }
    }
    public void GetNextPortalIndex( int nextSceneActivePortalIndex)
    {
        Debug.Log("3");
        nextActivePortalNumber = nextSceneActivePortalIndex;
        Debug.Log("4");
    }
    
    public void ReviveHere(Transform reviveMeHere)
    {
        Instantiate(player, reviveMeHere);
    }

    private void Update()
    {
        if (FindObjectsOfType<PlayerController>().Length < 1)
        {
            ReviveHere(currentRevivePoint);
        }
    }
}
