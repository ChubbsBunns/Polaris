using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    [Header("dont care")]
    private GameDataLog gameDataLog;
    public TMP_Text text;
    // Start is called before the first frame update
    public int gdlog_wallstuff;
    public int gdlog_doublejump;
    public int gdlog_dash;

    [Header("My settings")]
    public bool playCriteria_wallStuff;
    private int wallStuff;
    public bool playCriteria_doubleJump;
    private int doubleJump;
    public bool playCriteria_dash;
    private int dash;

    public float howManySecondsToWait;
    public bool doIStillPlay;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        gameDataLog = FindObjectOfType<GameDataLog>();
    }
    void Start()
    {
    }


    IEnumerator WaitForSecondsForDialogue()
    {
        yield return new WaitForSeconds(howManySecondsToWait);
        text.enabled = !text.enabled;
    }

    public void TriggerDialogue()
    {
        if (doIStillPlay)
        {
            text.enabled = !text.enabled;
            StartCoroutine(WaitForSecondsForDialogue());
            doIStillPlay = false;
        }
    }

}

