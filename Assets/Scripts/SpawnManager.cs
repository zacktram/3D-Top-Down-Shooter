using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;
    
    public GameObject[] spawnLocations;
    public GameObject[] enemyPrefabs;

    public GameObject lifeupPrefab;
    public GameObject powerupPrefab;
    public GameObject doublePointsPrefab;

    public int waveNumber = 1;
    public int enemyCount;
    private int extraEnemiesToSpawn = 2;

    private float spawnRangeX = 24;
    private float spawnRangeZ = 12;

    private float spawnPickupActual;
    private float pickupChanceToSpawnWave31 = 0.67f;
    private float pickupChanceToSpawn = 0.25f;
    private float midRangeWaveModifier = 0.1f;
    private float upperRangeWaveModifier = 0.25f;

    private int lowerBoundWaveNumber = 10;
    private int midBoundWaveNumber = 20;
    private int upperBoundWaveNumber = 30;
    // Start is called before the first frame update
    void Awake()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (gameManagerScript.gameOver == false)
        {
            SpawnEnemyWave(waveNumber);
        }

    }

    private Vector3 GenerateSpawnPosition()
    {

        int spawnLocationIndex = Random.Range(0, spawnLocations.Length);

        Vector3 randomPos = spawnLocations[spawnLocationIndex].transform.position;

        return randomPos;
    }

    private Vector3 GeneratePickupsPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);

        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        
        if(enemyCount == 0 && gameManagerScript.gameOver ==  false)
        {
            for(int i = 0; i < (enemiesToSpawn + extraEnemiesToSpawn); i++)
            {
                int enemyIndex = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
            }

            waveNumber++;
            SpawnPickups();

        }
  
    }

    void SpawnPickups()
    {
        if(GameObject.FindGameObjectsWithTag("Pickup").Length == 0)
        {
            CalculateChanceToSpawn(waveNumber);
        }
        
    }

    // Randomizes the chance pickups will spawn. Chance increases as waveNumber increases
    public void CalculateChanceToSpawn(int waveNumber)
    {
        if (waveNumber <= lowerBoundWaveNumber)
        {
            spawnPickupActual = Random.Range(0.0f, 1.0f);

            if (spawnPickupActual <= pickupChanceToSpawn)
            {
                Instantiate(powerupPrefab, GeneratePickupsPosition(), powerupPrefab.transform.rotation);
                Instantiate(lifeupPrefab, GeneratePickupsPosition(), lifeupPrefab.transform.rotation);
                Instantiate(doublePointsPrefab, GeneratePickupsPosition(), doublePointsPrefab.transform.rotation);
            }

        }
        else if (waveNumber > lowerBoundWaveNumber && waveNumber <= midBoundWaveNumber)
        {
            spawnPickupActual = Random.Range(0.0f, 1.0f);

            if (spawnPickupActual <= (pickupChanceToSpawn + midRangeWaveModifier))
            {
                Instantiate(powerupPrefab, GeneratePickupsPosition(), powerupPrefab.transform.rotation);
                Instantiate(lifeupPrefab, GeneratePickupsPosition(), lifeupPrefab.transform.rotation);
                Instantiate(doublePointsPrefab, GeneratePickupsPosition(), doublePointsPrefab.transform.rotation);
            }

        }
        else if (waveNumber > midBoundWaveNumber && waveNumber <= upperBoundWaveNumber)
        {
            spawnPickupActual = Random.Range(0.0f, 1.0f);

            if (spawnPickupActual <= (pickupChanceToSpawn + upperRangeWaveModifier))
            {
                Instantiate(powerupPrefab, GeneratePickupsPosition(), powerupPrefab.transform.rotation);
                Instantiate(lifeupPrefab, GeneratePickupsPosition(), lifeupPrefab.transform.rotation);
                Instantiate(doublePointsPrefab, GeneratePickupsPosition(), doublePointsPrefab.transform.rotation);
            }

        }
        else
        {

            spawnPickupActual = Random.Range(0.0f, 1.0f);

            if (spawnPickupActual <= pickupChanceToSpawnWave31)
            {
                Instantiate(powerupPrefab, GeneratePickupsPosition(), powerupPrefab.transform.rotation);
                Instantiate(lifeupPrefab, GeneratePickupsPosition(), lifeupPrefab.transform.rotation);
                Instantiate(doublePointsPrefab, GeneratePickupsPosition(), doublePointsPrefab.transform.rotation);
            }
        }

    }
}
