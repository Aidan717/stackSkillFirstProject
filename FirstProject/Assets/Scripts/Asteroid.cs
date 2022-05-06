using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speed = 20.0f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private SpawnManager _spawnManager;

    private void Start() {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.LogError("SpawnManager is null");
        }
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if ( other.tag == "Laser" ) {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
