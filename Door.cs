using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public float doorIndex;
    public bool amIOpen;
    public bool haveIBeenOpened;
    public bool switchHasBeenPressed;
    public GameObject door;
    public float openingSpeed;
    // Start is called before the first frame update

   public void openMe()
    {
        door.transform.position = Vector2.MoveTowards(door.transform.position, transform.position, openingSpeed);
        amIOpen = true;
        haveIBeenOpened = true;
    }
}
