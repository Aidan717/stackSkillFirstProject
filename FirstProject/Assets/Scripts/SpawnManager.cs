using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region "Enemy Field Region"
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private float _spawnTime = 5.0f;
    private bool _stopSpawning = false;

    #endregion

    #region "Powerup Field Region"
    [SerializeField]
    private GameObject[] _powerupPrefabs;

    [SerializeField]
    private float _spawnPowerTime = 5f;
    private bool _stopSpawningPowerups = false;

    #endregion


    public void StartSpawning() {
        StartCoroutine(SpawnTriplePowerup());
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnTriplePowerup() {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawningPowerups == false ) {
        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
        int randomPowerup = Random.Range(0,3);
        GameObject triplePowerup = Instantiate(_powerupPrefabs[randomPowerup], posToSpawn, Quaternion.identity);
        _spawnPowerTime = Random.Range(3, 8);
        yield return new WaitForSeconds(_spawnPowerTime);
        }
    }

    IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(3.0f);
        while ( _stopSpawning == false ) {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
        _stopSpawningPowerups = true;
    }

}
