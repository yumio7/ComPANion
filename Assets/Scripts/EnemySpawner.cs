using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private int waveCounter;
    [SerializeField] private int initialEnemyCount;
    private int _enemyCount;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject[] spawnersArea1;
    [SerializeField] private GameObject spawnersArea1Parent;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float timeBetweenWaves;


    //public GameObject[] spawnLocations;

    private GameObject _currentSpawner;

    private static EnemySpawner _instance;

    // TODO: make it so enemies cant spawn at location closest to chef    
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        spawnersArea1 = new GameObject[spawnersArea1Parent.transform.childCount];
        for (var i = 0; i < spawnersArea1Parent.transform.childCount; i++)
        {
            spawnersArea1[i] = spawnersArea1Parent.transform.GetChild(i).gameObject;
            //Debug.Log("For loop i value is " + i + "For loop child is " + spawnersArea1[i].name);
        }
        waveCounter = 1;
        _enemyCount = initialEnemyCount;
        SpawnNewEnemy();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private static void SpawnNewEnemy()
    {
        _instance.StartCoroutine(_instance.SpawnEnemy());
    }



    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SpawnEnemy()
    {
        if (_enemyCount != 0) {
            _currentSpawner = spawnersArea1[Random.Range(0, spawnersArea1.Length)];
            Debug.Log("Spawning new Enemy at " + _currentSpawner.name);
            Instantiate(enemy, _currentSpawner.transform.position, _currentSpawner.transform.rotation, _currentSpawner.transform);
            _enemyCount--;
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnNewEnemy();
            Debug.Log("Waited for Seconds");
        }
            //SpawnNewEnemy();
        else
        {
            Debug.Log("Starting Spawn for new Wave ");
            waveCounter += 1;
            _enemyCount = initialEnemyCount * waveCounter;
            yield return new WaitForSeconds(timeBetweenWaves);
            Debug.Log("Wave is " + waveCounter);
            SpawnNewEnemy();
        }
    }
}
