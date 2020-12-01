using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class colliderDialogue : MonoBehaviour
{
    public dialogue text_dialogue;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text_dialogue.TriggerDialogue();
        }
        else
        {
            return;
        }
    }
}
