using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] powerupsArray;
    [SerializeField] private GameObject[] goodFruitsArray;

    [SerializeField] private float _timeBetweenSpawnEnemy;
    [SerializeField] private float _timeBetweenSpawnGoodFruits;
    [SerializeField] private float _lowerLimit = 10, _upperLimit = 20;

    [SerializeField] private GameObject _spawnEnemyContainer;
    [SerializeField] private GameObject _spawnPowerupContainer;
    [SerializeField] private GameObject _spawnGoodFruitsContainer;

    private bool _stopSpawningEnemy = false;
    private bool _stopSpawningPowerup = false;
    private bool _stopSpawningGoodFruits = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRuotine(_timeBetweenSpawnEnemy));
        StartCoroutine(SpawnGoodFruits(_timeBetweenSpawnGoodFruits));
        StartCoroutine(SpawnPowerupRoutine(_lowerLimit, _upperLimit));
        StartCoroutine(PoweruplimitIncrementRoutine());
    }
    IEnumerator SpawnEnemyRuotine(float _timeBetweenSpawnEnemy)
    {
        while (_stopSpawningEnemy == false)
        {
            yield return new WaitForSeconds(2.0f);
            Vector3 newEnemyPos = new Vector3(Random.Range(-10.5f,11.0f), 8.0f, 0.0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, newEnemyPos, Quaternion.identity);
            //GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(0, 8, 0), Quaternion.identity);
            Debug.DrawLine(newEnemyPos, Vector3.down * 100, Color.blue);
            newEnemy.transform.parent = _spawnEnemyContainer.transform;
            yield return new WaitForSeconds(_timeBetweenSpawnEnemy);

        }
    }

    IEnumerator SpawnGoodFruits(float _timeBetweenSpawnGoodFruits)
    {
        while (_stopSpawningGoodFruits == false)
        {
            Vector3 newGoodFruitPos = new Vector3(Random.Range(-10.5f, 10.5f), 8, 0);
            int randomFruit = Random.Range(0, 7);
            GameObject newGoodFruit = Instantiate(goodFruitsArray[randomFruit], newGoodFruitPos, Quaternion.identity);
            newGoodFruit.transform.parent = _spawnGoodFruitsContainer.transform;
            yield return new WaitForSeconds(_timeBetweenSpawnGoodFruits);
        }
    }

    IEnumerator SpawnPowerupRoutine(float _lowerLimit, float _upperLimit)
    {
        while (_stopSpawningPowerup == false)
        {
        yield return new WaitForSeconds(Random.Range(_lowerLimit, _upperLimit));
        Vector3 newPowerupPos = new Vector3(Random.Range(-10.5f, 10.5f), 8, 0);
        int randomPowerup = Random.Range(0, 3);
        GameObject newPowerup = Instantiate(powerupsArray[randomPowerup], newPowerupPos, Quaternion.identity);
        newPowerup.transform.parent = _spawnPowerupContainer.transform;
        }    
    }

    IEnumerator PoweruplimitIncrementRoutine()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        while (player != null)
        {
            yield return new WaitForSeconds(10);
            _upperLimit = _upperLimit + 1.0f;
            _lowerLimit = _lowerLimit + 1.0f;
            if(_timeBetweenSpawnEnemy > 1f)
            {
                _timeBetweenSpawnEnemy = _timeBetweenSpawnEnemy - 0.05f;
            }
            if(_timeBetweenSpawnGoodFruits > 1f)
            {
                _timeBetweenSpawnGoodFruits = _timeBetweenSpawnGoodFruits - 0.05f;
            }
        }
    }

    public void OnPlayerDeath()
    { 
        _stopSpawningEnemy = true;
        _stopSpawningPowerup = true;
        _stopSpawningGoodFruits = true;
    }

}
