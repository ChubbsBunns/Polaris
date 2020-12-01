using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBeltEffector : MonoBehaviour
{
    [SerializeField] PlayerController player;
    public bool imAHorizontalRightAccelerator;
    public bool imAHorizontalLeftAccelerator;
    public bool imAVerticalDownAccelerator;
    public bool imAVerticalUpAccelerator;
    public float boostSpeedOnInObjects;
    // Start is called before the first frame update
    void Start()
    {
     player =    FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

            if (imAHorizontalRightAccelerator == true)
        {
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x + boostSpeedOnInObjects, collision.attachedRigidbody.velocity.y);
        }

            else if (imAHorizontalLeftAccelerator == true)
        {
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x - boostSpeedOnInObjects, collision.attachedRigidbody.velocity.y);
        }

            else if (imAVerticalUpAccelerator == true)
        {
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, collision.attachedRigidbody.velocity.y + boostSpeedOnInObjects);
        }

            else if (imAVerticalDownAccelerator == true)
        {
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, collision.attachedRigidbody.velocity.y - boostSpeedOnInObjects);
        }

            if (collision.gameObject.tag == "Player")
        {
            PlayerController playerGameObject = collision.gameObject.GetComponent<PlayerController>();
            {
                playerGameObject.acceleratorMovementSpeedBoost = boostSpeedOnInObjects;
                if (imAHorizontalRightAccelerator == true)
                {
                    playerGameObject.imOnAHorizontalRightAccelerator = true;
                }

                else if (imAHorizontalLeftAccelerator == true)
                {
                    playerGameObject.imOnAHorizontalLeftAccelerator = true;
                }

                else if (imAVerticalUpAccelerator == true)
                {
                    playerGameObject.imOnAVerticalUpAccelerator = true;
                }

                else if (imAVerticalDownAccelerator == true)
                {
                    playerGameObject.imOnAVerticalDownAccelerator = true;
                }
            }

        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = null;
            PlayerController playerGameObject = collision.gameObject.GetComponent<PlayerController>();
            {
               playerGameObject.imOnAHorizontalLeftAccelerator = false;
                playerGameObject.imOnAHorizontalRightAccelerator = false;
                playerGameObject.imOnAVerticalDownAccelerator = false;
                playerGameObject.imOnAVerticalUpAccelerator = false;
            }

        }
    }      
}
