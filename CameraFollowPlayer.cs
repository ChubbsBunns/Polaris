using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform tFollowTarget;
    public CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    private void Awake()
    {
        if (FindObjectsOfType<CameraFollowPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = FindObjectOfType<PlayerController>().gameObject;
        var vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;


    }


    public void FollowPlayer()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        var vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
