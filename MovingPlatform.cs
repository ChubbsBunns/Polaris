using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool myFirstPointIsSet;
    public GameObject[] patrolPoints;
    public PatrolPoint currentPatrolPoint;
    public Transform theFirstPointIMoveTo;
    public Transform nextPointIAmMovingTo;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        myFirstPointIsSet = false;
        patrolPoints = GameObject.FindGameObjectsWithTag("Patrol Point");
        nextPointIAmMovingTo.position = theFirstPointIMoveTo.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPointIAmMovingTo.position, speed * Time.deltaTime);
    }

    public void MoveToNextPoint(Transform myNextPoint)
    {
        nextPointIAmMovingTo.position = myNextPoint.position;
        myFirstPointIsSet = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = transform;
        }

        if (collision.tag == "Patrol Point")
        {
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
