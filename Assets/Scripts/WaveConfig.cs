using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyprefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float movingSpeed = 2f;
    [SerializeField] float spawnRate = 0.3f;
    [SerializeField] float numberOfEnemies = 5;
    [SerializeField] float timeBetweenSpawns = 0.5f;

    public GameObject GetEnemyPrefab() { return enemyprefab; }
    public GameObject GetPathPrefab() { return pathPrefab; }
    public float GetSpawnRate() { return spawnRate; }
    public float GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return movingSpeed; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public List<Transform> GetEnemyPosition()
    {
        var newList = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            newList.Add(child);
        }
        return newList;
    }



}
