using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    private int waveIndex = 0;
    void Start()
    {

    }


    void Update()
    {
        //SpawnAllEnemies(waveConfigs[waveIndex]);
    }







    private void SpawnAllEnemies(WaveConfig waveConfig)
    {
        Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetEnemyPosition()[0].position, transform.rotation);
    }
}
