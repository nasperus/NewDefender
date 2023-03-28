using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    private int indexPos = 0;


    void Start()
    {
        waypoints = waveConfig.GetPathPosition();
        transform.position = waypoints[indexPos].position;


    }


    void Update()
    {
        EnemyMoveToPosition();

    }



    public void ThisObject(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void EnemyMoveToPosition()
    {
        if (indexPos <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[indexPos].transform.position;
            var moveTarget = waveConfig.GetMoveSpeed() * Time.deltaTime;
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
