using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public PlayerController playerObject;
    public GameDataLog gameDataLog;
    public string sceneNameToLoad;
    public int portalIndex;
    public int nextSceneActivePortalIndex;
    public Transform portalLocation;
    // Start is called before the first frame update
    private void Awake()
    {
    }

    void Start()
    {
        Debug.Log("7");
       playerObject =  FindObjectOfType<PlayerController>();
        Debug.Log("8");
        gameDataLog = FindObjectOfType<GameDataLog>();

        PlayerInstantiateOrNot();
    }


    public void FindMyObjects()
    {
        playerObject = FindObjectOfType<PlayerController>();
        gameDataLog = FindObjectOfType<GameDataLog>();

    }

    private void PlayerInstantiateOrNot()
    {
        if (gameDataLog.nextActivePortalNumber == portalIndex)
        {
            Debug.Log("9");
            if (portalLocation == null)
            {
                Debug.Log("portal location not here");
                Debug.Log("10");
                return;
            }
            Debug.Log("11");
            playerObject.InstantiateMeHere(portalLocation);
            Debug.Log("12");
        }
        else
        {
            return;
        }
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
        if (collision.CompareTag ("Player"))
        {
            Debug.Log("2");
            gameDataLog.GetNextPortalIndex(nextSceneActivePortalIndex);
            Debug.Log("5");
            SceneManager.LoadScene(sceneNameToLoad);
            Debug.Log("6");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(portalLocation.position, 0.7f);
        Gizmos.DrawSphere(transform.position, 0.4f);
    }
}
