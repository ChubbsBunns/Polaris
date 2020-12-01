using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikesWait : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
   public int currentPointIndex;
    public bool move;
    public float timeToWaitToMove;

   public float waitTime;
    public float startWaitTime;
    // Start is called before the first frame update

    private void Awake()
    {
        transform.position = patrolPoints[0].position;
    }
    void Start()
    {
        StartCoroutine(NowYouMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                if (waitTime <= 0)
                {
                    if (currentPointIndex + 1 < patrolPoints.Length)
                    {
                        currentPointIndex++;
                    }
                    else
                    {
                        currentPointIndex = 0;
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }

            }





        }
    }

    IEnumerator NowYouMove()
    {
        yield return new WaitForSeconds(timeToWaitToMove);
        move = true;
    }
}
