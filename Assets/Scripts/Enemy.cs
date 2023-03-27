using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    private int indexPos = 0;
    private float enemySpeed = 3f;

    void Start()
    {
        waypoints = waveConfig.GetEnemyPosition();
        transform.position = waypoints[indexPos].position;

    }


    void Update()
    {
        EnemyMoveToPosition();
    }


    private void EnemyMoveToPosition()
    {
        if (indexPos <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[indexPos].transform.position;
            var moveTarget = enemySpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveTarget);

            if (transform.position == targetPosition)
            {
                indexPos++;
            }
        }

        else
        {
            Destroy(gameObject);
        }

    }


}
