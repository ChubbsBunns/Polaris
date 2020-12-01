using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public int patrolPointIndex;
    public MovingPlatform[] movingPlatforms;

    private void Update()
    {
       movingPlatforms = FindObjectsOfType<MovingPlatform>();
        foreach (MovingPlatform platform in movingPlatforms)
        {
            if (platform.myFirstPointIsSet == false)
            platform.MoveToNextPoint(transform);
        }
       
    }
}
