using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool check;


    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(AllPaths());
        }

        while (check);  
       
    }


   
    IEnumerator AllPaths()
    {
        for (int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }


    IEnumerator SpawnAllEnemies (WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetEnemyPosition()[0].transform.position, transform.rotation);
            newEnemy.GetComponent<Enemy>().ThisObject(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());

        }
    }



}
