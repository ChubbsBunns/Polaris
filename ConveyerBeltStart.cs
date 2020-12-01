using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBeltStart : MonoBehaviour
{
    public GameObject beltTiles;
    public bool dontCreateMeYet;
    public Transform boxAppearsHere;
    public float timeBetweenSpawns;


    // Update is called once per frame
    void Update()
    {
        if (dontCreateMeYet)
        {
            StartCoroutine(InstantiateTile());
        }
        else
        {
            return;
        }
    }

    IEnumerator InstantiateTile()
    {
        dontCreateMeYet = false;
        _ = Instantiate(beltTiles, boxAppearsHere);
        yield return new WaitForSeconds(timeBetweenSpawns);
        dontCreateMeYet = true;
    }
}
