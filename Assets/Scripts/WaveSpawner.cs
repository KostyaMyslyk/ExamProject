using System.Collections;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;


    public Transform spawnPoint1;
    public Transform spawnPoint2;


    public Transform[] path1Waypoints;
    public Transform[] path2Waypoints;


    public Health baseHealth;


    public ResourceManager resourceManager;


    public GameManager gameManager;



    public int currentWave = 1;

    public int maxWaves = 10;


    public float spawnDelay = 1f;



    private int aliveEnemies = 0;



    
    void Start()
    {
        if (gameManager != null)
        {
            gameManager.maxWaves = maxWaves;
            gameManager.SetWave(currentWave);
        }


        StartCoroutine(WaitForFaction());
    }



    IEnumerator WaitForFaction()
    {
        while (FactionManager.Instance == null ||
              !FactionManager.Instance.factionChosen)
        {
            yield return null;
        }


        StartCoroutine(StartWaveLoop());
    }




    IEnumerator StartWaveLoop()
    {
        while (currentWave <= maxWaves)
        {
            yield return SpawnWave();


            while (aliveEnemies > 0)
            {
                yield return null;
            }


            if (baseHealth != null &&
                !baseHealth.IsAlive())
            {
                yield break;
            }


            currentWave++;


            if (currentWave <= maxWaves)
            {
                if (gameManager != null)
                {
                    gameManager.SetWave(currentWave);
                }

                yield return new WaitForSeconds(2f);
            }
        }


        if (baseHealth != null &&
            baseHealth.IsAlive())
        {
            gameManager.WinGame();
        }
    }





    IEnumerator SpawnWave()
    {
        int enemiesInWave = currentWave;


        for (int i = 0; i < enemiesInWave; i++)
        {
            bool usePath1 =
                Random.Range(0, 2) == 0;


            Transform selectedSpawn =
                usePath1
                ? spawnPoint1
                : spawnPoint2;


            Transform[] selectedPath =
                usePath1
                ? path1Waypoints
                : path2Waypoints;


            GameObject enemy =
                Instantiate(
                    enemyPrefab,
                    selectedSpawn.position,
                    Quaternion.identity
                );


            aliveEnemies++;


            EnemyMovement movement =
                enemy.GetComponent<EnemyMovement>();


            if (movement != null)
            {
                movement.Init(
                    selectedPath,
                    baseHealth
                );
            }


           

            EnemyHealth health =
    enemy.GetComponent<EnemyHealth>();


            if (health != null)
            {

                int randomType =
                    Random.Range(0, 100);



                if (randomType < 75)
                {
                    health.SetupType(
                        EnemyHealth.EnemyType.Normal
                    );
                }
                else if (randomType < 90)
                {
                    health.SetupType(
                        EnemyHealth.EnemyType.Fast
                    );
                }
                else
                {
                    health.SetupType(
                        EnemyHealth.EnemyType.Tank
                    );
                }



                health.resourceManager =
                    resourceManager;


                health.spawner =
                    this;
            }


            yield return new WaitForSeconds(spawnDelay);
        }
    }




    public void EnemyDied()
    {
        aliveEnemies--;
    }
}