using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public GameDataLog gdLog;
    public CinemachineVirtualCamera cm_cam;
    // Start is called before the first frame update
    void Start()
    {
      gdLog =   FindObjectOfType<GameDataLog>();
      cm_cam =  FindObjectOfType<CinemachineVirtualCamera>();
        Destroy(gdLog);
        Destroy(cm_cam);
        StartCoroutine(waitForAFewSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitForAFewSeconds()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("Main Menu");
    }
}
